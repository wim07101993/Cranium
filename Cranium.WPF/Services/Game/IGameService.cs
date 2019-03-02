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

        Task<Game> CreateAsync(TimeSpan gameTime, IEnumerable<Player> players);
        Task<Game> CreateAsync(int cycleCount, IEnumerable<Player> players);

        Task<Game> MovePlayerTo(ObjectId playerId, ObjectId categoryId);
        Task<bool> IsAtEnd(ObjectId playerId);

        Task<Question> GetQuestionAsync(ObjectId playerId);
        Task<IEnumerable<Answer>> GetAnswers(ObjectId questionId);


        event EventHandler GameChangedEvent;
    }
}