using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cranium.WPF.Data.Answer;
using MongoDB.Bson;

namespace Cranium.WPF.Game
{
    public interface IGameService
    {
        Game Game { get; }


        Task SaveGameAsync();
        Task<Game> LoadGameAsync(ObjectId gameId);

        Task<Game> CreateAsync(TimeSpan gameTime, IReadOnlyList<Player.Player> players);
        Task<Game> CreateAsync(int cycleCount, IReadOnlyList<Player.Player> players);

        Task<Game> MovePlayerToAsync(ObjectId playerId, ObjectId categoryId);
        Task<Game> MovePlayerBackwardsToAsync(ObjectId playerId, ObjectId categoryId);
        Task<bool> IsAtEnd(ObjectId playerId);

        Task<Data.Question.Question> GetQuestionAsync();
        Task<Data.Question.Question> GetQuestionAsync(ObjectId categoryId);
        Task<IEnumerable<Answer>> GetAnswers(ObjectId questionId);

        Task NextTurnAsync();


        event EventHandler GameChangedEvent;
        event EventHandler PlayerChangedEvent;
    }
}