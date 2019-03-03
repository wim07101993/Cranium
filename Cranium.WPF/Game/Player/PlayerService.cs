using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shared.Extensions;

namespace Cranium.WPF.Game.Player
{
    public class PlayerService : IPlayerService
    {
        #region FIELDS

        private readonly IGameService _gameService;

        #endregion FIELDS


        #region CONSTRUCTOR

        public PlayerService(IGameService gameService)
        {
            _gameService = gameService;
        }

        #endregion CONSTRUCTOR


        #region METHODS

        public Task<Player> CreateAsync(Player item)
        {
            if (_gameService.Game == null)
                return null;

            item.Id = ObjectId.GenerateNewId();
            _gameService.Game.Players.Add(item);
            return Task.FromResult(item);
        }

        public Task AddItemToListProperty<TValue>(
            ObjectId id, Expression<Func<Player, IEnumerable<TValue>>> propertyToAddItemTo, TValue itemToAdd)
        {
            var i = _gameService.Game.Players.IndexOfFirst(x => x.Id == id);
            ((IList<TValue>)propertyToAddItemTo.Compile()(_gameService.Game.Players[i])).Add(itemToAdd);
            return Task.CompletedTask;
        }

        public Task<TOut> GetPropertyAsync<TOut>(ObjectId id, Expression<Func<Player, TOut>> propertyToSelect)
            => Task.FromResult(
                _gameService.Game.Players
                    .Where(x => x.Id == id)
                    .Select(propertyToSelect.Compile())
                    .FirstOrDefault());

        public Task<Player> GetOneAsync(
            ObjectId id, IEnumerable<Expression<Func<Player, object>>> propertiesToInclude = null)
            => Task.FromResult(_gameService.Game.Players.FirstOrDefault(x => x.Id == id));

        public Task<IList<Player>> GetAsync(IEnumerable<Expression<Func<Player, object>>> propertiesToInclude = null)
            => Task.FromResult(_gameService.Game.Players as IList<Player>);

        public Task<Player> GetByAsync(
            Expression<Func<Player, bool>> condition,
            IEnumerable<Expression<Func<Player, object>>> propertiesToInclude = null)
            => Task.FromResult(_gameService.Game.Players.FirstOrDefault(condition.Compile()));

        public Task<TOut> GetPropertyByAsync<TOut>(
            Expression<Func<Player, bool>> condition, Expression<Func<Player, TOut>> propertyToSelect)
            => Task.FromResult(_gameService.Game.Players
                .Where(condition.Compile())
                .Select(propertyToSelect.Compile())
                .FirstOrDefault());

        public async Task UpdateAsync(Player newItem, IEnumerable<Expression<Func<Player, object>>> propertiesToUpdate = null)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceAsync(Player newItem)
        {
            var i = _gameService.Game.Players.IndexOfFirst(x => x.Id == newItem.Id);
            _gameService.Game.Players[i] = newItem;
            return Task.CompletedTask;
        }

        public Task UpdatePropertyAsync<TValue>(
            ObjectId id, Expression<Func<Player, TValue>> propertyToUpdate, TValue value)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(ObjectId id)
        {
            _gameService.Game.Players.RemoveFirst(x => x.Id == id);
            return Task.CompletedTask;
        }

        public Task RemoveItemFromList<TValue>(
            ObjectId id, Expression<Func<Player, IEnumerable<TValue>>> propertyToRemoveItemFrom, TValue itemToRemove)
        {
            var i = _gameService.Game.Players.IndexOfFirst(x => x.Id == id);
            ((IList<TValue>)propertyToRemoveItemFrom.Compile()(_gameService.Game.Players[i])).Remove(itemToRemove);
            return Task.CompletedTask;
        }

        #endregion METHODS
    }
}