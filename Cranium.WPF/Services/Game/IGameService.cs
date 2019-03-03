using Cranium.WPF.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cranium.WPF.Services.Game
{
    public interface IGameService
    {
        Game Game { get; }


        Task SaveGameAsync();
        Task<Game> LoadGameAsync(ObjectId gameId);

        Task<Game> CreateAsync(TimeSpan gameTime, IReadOnlyList<Player> players);
        Task<Game> CreateAsync(int cycleCount, IReadOnlyList<Player> players);

        Task<Game> MovePlayerToAsync(ObjectId playerId, ObjectId categoryId);
        Task<Game> MovePlayerBackwardsToAsync(ObjectId playerId, ObjectId categoryId);
        Task<bool> IsAtEnd(ObjectId playerId);

        Task<Question> GetQuestionAsync();
        Task<Question> GetQuestionAsync(ObjectId categoryId);
        Task<IEnumerable<Answer>> GetAnswers(ObjectId questionId);

        Task NextTurnAsync();


        event EventHandler GameChangedEvent;
        event EventHandler PlayerChangedEvent;
    }
}