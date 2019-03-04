using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Cranium.WPF.Data.Answer;
using Cranium.WPF.Data.Category;
using Cranium.WPF.Helpers;
using MongoDB.Bson;

namespace Cranium.WPF.Game
{
    public interface IGameService
    {
        #region PROPERTIES

        GameBoard.GameBoard GameBoard { get; }

        ReadOnlyObservableCollection<Player.Player> Players { get; }
        ReadOnlyObservableCollection<Category> Categories { get; }
        ReadOnlyObservableCollection<Data.Question.Question> Questions { get; }
        ReadOnlyObservableCollection<Data.Question.Question> AnsweredQuestions { get; }

        Player.Player CurrentPlayer { get; }
        Tile.Tile TileOfCurrentPlayer { get; }

        #endregion PROPERTIES


        #region METHODS

        #region game creation

        Task SaveGameAsync();
        Task LoadGameAsync(ObjectId gameId);

        Task CreateAsync(TimeSpan gameTime);
        Task CreateAsync(int cycleCount);

        #endregion game creation

        #region players

        Task AddPlayersAsync(params Player.Player[] players);
        Task RemovePlayersAsync(params ObjectId[] playerIds);
        
        Task MovePlayerToAsync(ObjectId playerId, ObjectId categoryId);
        Task MovePlayerBackwardsToAsync(ObjectId playerId, ObjectId categoryId);
        Task MovePlayerToAsync(ObjectId playerId, int tileIndex);
        Task<bool> IsAtEnd(ObjectId playerId);

        #endregion players

        #region questions

        Task<Data.Question.Question> GetQuestionAsync();
        Task<Data.Question.Question> GetQuestionAsync(ObjectId categoryId);
        Task<IEnumerable<Answer>> GetAnswers(ObjectId questionId);

        #endregion questions

        #region turns

        Task NextTurnAsync();

        #endregion turns

        #endregion METHODS


        #region EVENTS

        event AsyncEventHandler GameChanged;
        event AsyncEventHandler PlayerChanged;
        event AsyncGameFinishedEventHandler GameFinished;

        #endregion EVENTS
    }
}