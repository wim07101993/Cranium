using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Cranium.WPF.Data.Answer;
using Cranium.WPF.Data.Category;
using Cranium.WPF.Data.Question;
using Cranium.WPF.Game.Player;
using Cranium.WPF.Game.Tile;
using Cranium.WPF.Helpers;
using Cranium.WPF.Helpers.Extensions;
using MongoDB.Bson;
using Prism.Mvvm;
using Shared.Extensions;

namespace Cranium.WPF.Game
{
    public class GameService : BindableBase, IGameService
    {
        #region FIELDS

        private static readonly Random Random = new Random();
        private static readonly TimeSpan TimePerCycle = TimeSpan.FromMinutes(12);

        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;
        private readonly IMongoGameService _mongoGameService;

        private readonly ObservableCollection<Player.Player> _players =
            new ObservableCollection<Player.Player>();

        private readonly ObservableCollection<Data.Question.Question> _questions =
            new ObservableCollection<Data.Question.Question>();

        private readonly ObservableCollection<Data.Question.Question> _answeredQuestions =
            new ObservableCollection<Data.Question.Question>();

        private readonly ObservableCollection<Category> _categories =
            new ObservableCollection<Category>();

        private int _currentPlayer;
        private GameBoard.GameBoard _gameBoard;

        #endregion FIELDS


        #region CONSTRUCTORS

        public GameService(
            ICategoryService categoryService, IQuestionService questionService, IMongoGameService mongoGameService)
        {
            _categoryService = categoryService;
            _questionService = questionService;
            _mongoGameService = mongoGameService;
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES

        public GameBoard.GameBoard GameBoard
        {
            get => _gameBoard;
            private set => SetProperty(ref _gameBoard, value);
        }

        public ReadOnlyObservableCollection<Player.Player> Players
            => new ReadOnlyObservableCollection<Player.Player>(_players);

        public ReadOnlyObservableCollection<Category> Categories
            => new ReadOnlyObservableCollection<Category>(_categories);

        public ReadOnlyObservableCollection<Data.Question.Question> Questions
            => new ReadOnlyObservableCollection<Data.Question.Question>(_questions);

        public ReadOnlyObservableCollection<Data.Question.Question> AnsweredQuestions
            => new ReadOnlyObservableCollection<Data.Question.Question>(_answeredQuestions);

        public Player.Player CurrentPlayer => Players.Count > 0 ? Players[_currentPlayer] : null;

        public Tile.Tile TileOfCurrentPlayer
            => GameBoard?.First(tile => tile.Players.Any(player => player.Id == CurrentPlayer.Id));

        #endregion PROPERTIES


        #region METHODS

        #region game creation

        public async Task SaveGameAsync()
            => await _mongoGameService.CreateAsync(new Game
            {
                AnsweredQuestions = AnsweredQuestions,
                CurrentPlayerIndex = _currentPlayer,
                GameBoard = GameBoard,
                Players = Players,
                Questions = Questions
            });

        public async Task LoadGameAsync(ObjectId gameId)
        {
            var game = await _mongoGameService.GetOneAsync(gameId);
            GameBoard = game.GameBoard;
            _currentPlayer = game.CurrentPlayerIndex;

            _players.Clear();
            _players.Add(game.Players);

            _questions.Clear();
            _questions.Add(game.Questions);

            _answeredQuestions.Clear();
            _answeredQuestions.Add(game.AnsweredQuestions);

            await RefreshCategoriesAsync();

            if (GameChanged != null)
                await GameChanged(this);
        }

        public async Task CreateAsync(TimeSpan gameTime)
        {
            var cycleCount = (int) Math.Round(gameTime.TotalSeconds / TimePerCycle.TotalSeconds);
            await CreateAsync(cycleCount);
        }

        public async Task CreateAsync(int cycleCount)
        {
            // remove old questions
            _questions.Clear();
            _answeredQuestions.Clear();

            // add new questions
            _questions.Add(await _questionService.GetAsync());

            // refresh categories
            await RefreshCategoriesAsync();

            // generate game-board
            var tiles = CreateTiles(cycleCount, Categories);
            GameBoard = new GameBoard.GameBoard(tiles);

            if (GameChanged != null)
                await GameChanged.Invoke(this);
        }

        private static IEnumerable<Tile.Tile> CreateTiles(int cycleCount, IReadOnlyList<Category> readOnlyCategories)
        {
            if (readOnlyCategories == null || readOnlyCategories.Count == 0)
                throw new NoCategoriesException();

            var categories = readOnlyCategories.ToList();

            var specialCategory = categories.FirstOrDefault(x => x.IsSpecial);
            if (specialCategory == null)
                throw new NoSpecialCategoryException();

            categories.RemoveWhere(x => x.IsSpecial);

            for (var i = 0; i < cycleCount; i++)
            {
                yield return new Tile.Tile(ObjectId.GenerateNewId(), specialCategory.Id);

                foreach (var category in categories)
                    yield return new Tile.Tile(ObjectId.GenerateNewId(), category.Id);
            }

            yield return new Tile.Tile(ObjectId.GenerateNewId(), specialCategory.Id);
        }

        private async Task RefreshCategoriesAsync()
        {
            _categories.Clear();
            _categories.Add(new List<Category>
            {
                _questions
                    .Select(x => x.QuestionType.Category)
                    .Distinct(new CategoryIdComparer())
                    .ToList(),
                _answeredQuestions
                    .Select(x => x.QuestionType.Category)
                    .Distinct(new CategoryIdComparer())
                    .ToList(),
                await _categoryService.GetByAsync(x => x.IsSpecial),
            });
        }

        #endregion game creation

        #region players

        public Task AddPlayersAsync(params Player.Player[] players)
        {
            if (players == null)
                throw new ArgumentNullException(nameof(players));

            if (GameBoard == null)
                throw new GameException("There is not yet a game created, cannot add players");

            if (GameBoard.Count < 1)
                throw new GameException("The game has no tiles, cannot add players");

            GameBoard[0].Players.Add(players);
            _players.Add(players);
            return Task.CompletedTask;
        }

        public Task RemovePlayersAsync(params ObjectId[] playerIds)
        {
            if (playerIds == null)
                throw new ArgumentNullException(nameof(playerIds));

            if (GameBoard == null)
                throw new GameException("There is not yet a game created, cannot remove players");

            if (GameBoard.Count < 1)
                throw new GameException("The game has no tiles, cannot remove players");

            var playerIdList = playerIds.ToList();
            foreach (var tile in GameBoard)
            {
                for (var i = 0; i < tile.Players.Count; i++)
                {
                    var localI = i;
                    if (!playerIdList.RemoveFirst(x => tile.Players[localI].Id == x))
                        continue;

                    var id = tile.Players[i].Id;
                    tile.Players.RemoveAt(i);
                    _players.RemoveFirst(x => x.Id == id);

                    if (playerIdList.Count == 0)
                        return Task.CompletedTask;
                    i--;
                }
            }

            throw new PlayerNotFoundException("Could not remove all players since some do not play in this game.");
        }

        public async Task MovePlayerToAsync(ObjectId playerId, ObjectId categoryId)
        {
            Player.Player player = null;
            foreach (var tile in GameBoard)
            {
                if (player == null)
                {
                    var playerIndex = tile.Players.IndexOfFirst(x => x.Id == playerId);
                    if (playerIndex >= 0)
                    {
                        player = tile.Players[playerIndex];
                        tile.Players.RemoveAt(playerIndex);
                    }
                }
                else
                {
                    var dbCategoryId = await _categoryService.GetPropertyAsync(tile.CategoryId, x => x.Id);
                    if (dbCategoryId == categoryId)
                    {
                        tile.Players.Add(player);
                        return;
                    }
                }
            }

            if (player == null)
                throw new PlayerNotFoundException();
            throw new TileNotFoundException();
        }

        public async Task MovePlayerBackwardsToAsync(ObjectId playerId, ObjectId categoryId)
        {
            Player.Player player = null;

            var i = 0;
            for (; i < GameBoard.Count; i++)
            {
                var tile = GameBoard[i];
                var playerIndex = tile.Players.IndexOfFirst(x => x.Id == playerId);
                if (playerIndex < 0)
                    continue;

                player = tile.Players[playerIndex];
                tile.Players.RemoveAt(playerIndex);
            }

            if (player == null)
                throw new PlayerNotFoundException();

            for (; i >= 0; i--)
            {
                var tile = GameBoard[i];
                var dbCategoryId = await _categoryService.GetPropertyAsync(tile.CategoryId, x => x.Id);
                if (dbCategoryId == categoryId)
                {
                    tile.Players.Add(player);
                    return;
                }
            }

            throw new TileNotFoundException();
        }

        public Task MovePlayerToAsync(ObjectId playerId, int tileIndex)
        {
            Player.Player player = null;
            foreach (var tile in GameBoard)
            {
                var playerIndex = tile.Players.IndexOfFirst(x => x.Id == playerId);
                if (playerIndex >= 0)
                {
                    player = tile.Players[playerIndex];
                    tile.Players.RemoveAt(playerIndex);
                    break;
                }
            }

            if (player == null)
                throw new PlayerNotFoundException();

            if (tileIndex < 0 || tileIndex > GameBoard.Count)
                throw new TileNotFoundException();

            GameBoard[tileIndex].Players.Add(player);
            return Task.CompletedTask;
        }

        public Task<bool> IsAtEnd(ObjectId playerId)
        {
            return Task.FromResult(GameBoard.Last().Players.Any(x => x.Id == playerId));
        }

        #endregion players

        #region questions

        public async Task<Data.Question.Question> GetQuestionAsync()
            => await GetQuestionAsync(TileOfCurrentPlayer.CategoryId);

        public async Task<Data.Question.Question> GetQuestionAsync(ObjectId categoryId)
        {
            var isSpecialCategory = await _categoryService.GetPropertyAsync(categoryId, x => x.IsSpecial);
            if (isSpecialCategory)
                return null;

            var question = _questions.First(x => x.QuestionType.Category.Id == categoryId);
            _questions.RemoveFirst(x => x.Id == question.Id);
            _answeredQuestions.Add(question);
            return question;
        }

        public async Task<IEnumerable<Answer>> GetAnswers(ObjectId questionId)
        {
            var question = await _questionService.GetOneAsync(questionId);
            return question.Answers;
        }

        #endregion questions

        #region turns

        public async Task NextTurnAsync()
        {
            _currentPlayer =
                _currentPlayer + 1 > Players.Count
                    ? 0
                    : _currentPlayer + 1;

            if (PlayerChanged != null)
                await PlayerChanged.Invoke(this);

            RaisePropertyChanged(nameof(CurrentPlayer));
            RaisePropertyChanged(nameof(TileOfCurrentPlayer));
        }

        #endregion turns

        #endregion METHODS


        #region EVENTS

        public event AsyncEventHandler GameChanged;
        public event AsyncEventHandler PlayerChanged;
        public event AsyncGameFinishedEventHandler GameFinished;

        #endregion EVENTS


        #region CLASSES

        private class CategoryIdComparer : IEqualityComparer<Category>
        {
            public bool Equals(Category x, Category y) => x != null && x.Id == y?.Id;

            public int GetHashCode(Category category) => category.Id.GetHashCode();
        }

        #endregion CLASSES
    }
}