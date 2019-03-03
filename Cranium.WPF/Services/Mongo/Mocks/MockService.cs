using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Cranium.WPF.Models;
using MongoDB.Bson;
using Shared.Extensions;

namespace Cranium.WPF.Services.Mongo.Mocks
{
    public class MockService : ICategoryService
    {
        private List<Category> _categories = new List<Category>()
        {
            new Category
            {
                Color = new Models.Color {BaseColor = Colors.Blue},
                Id = ObjectId.GenerateNewId(),
                Name = "Poekie Palet"
            },
            new Category
            {
                Color = new Models.Color {BaseColor = Colors.Green},
                Id = ObjectId.GenerateNewId(),
                Name = "Show Spetter"
            },
            new Category
            {
                Color = new Models.Color {BaseColor = Colors.Yellow},
                Id = ObjectId.GenerateNewId(),
                Name = "Woord Wurm"
            },
            new Category
            {
                Color = new Models.Color {BaseColor = Colors.Red},
                Id = ObjectId.GenerateNewId(),
                Name = "Knappe Kop"
            }
        };


        public Task AddItemToListProperty<TValue>(
            ObjectId id, Expression<Func<Category, IEnumerable<TValue>>> propertyToAddItemTo, TValue itemToAdd)
        {
            var i = _categories.IndexOfFirst(x => x.Id == id);
            ((IList<TValue>) propertyToAddItemTo.Compile()(_categories[i])).Add(itemToAdd);
            return Task.CompletedTask;
        }

        public Task<Category> CreateAsync(Category item)
        {
            item.Id = ObjectId.GenerateNewId();
            _categories.Add(item);
            return Task.FromResult(item);
        }

        public Task<IList<Category>> GetAsync(
            IEnumerable<Expression<Func<Category, object>>> propertiesToInclude = null)
            => Task.FromResult(_categories as IList<Category>);

        public Task<Category> GetByAsync(
            Expression<Func<Category, bool>> condition,
            IEnumerable<Expression<Func<Category, object>>> propertiesToInclude = null)
            => Task.FromResult(_categories.FirstOrDefault(condition.Compile()));

        public Task<TOut> GetPropertyByAsync<TOut>(
            Expression<Func<Category, bool>> condition, Expression<Func<Category, TOut>> propertyToSelect)
            => Task.FromResult(_categories
                .Where(condition.Compile())
                .Select(propertyToSelect.Compile())
                .FirstOrDefault());

        public Task<Category> GetOneAsync(
            ObjectId id, IEnumerable<Expression<Func<Category, object>>> propertiesToInclude = null)
            => Task.FromResult(_categories.FirstOrDefault(x => x.Id == id));

        public Task<TOut> GetPropertyAsync<TOut>(ObjectId id, Expression<Func<Category, TOut>> propertyToSelect)
            => Task.FromResult(
                _categories
                    .Where(x => x.Id == id)
                    .Select(propertyToSelect.Compile())
                    .FirstOrDefault());

        public Task RemoveAsync(ObjectId id)
        {
            _categories.RemoveFirst(x => x.Id == id);
            return Task.CompletedTask;
        }

        public Task RemoveItemFromList<TValue>(
            ObjectId id, Expression<Func<Category, IEnumerable<TValue>>> propertyToRemoveItemFrom, TValue itemToRemove)
        {
            var i = _categories.IndexOfFirst(x => x.Id == id);
            ((IList<TValue>) propertyToRemoveItemFrom.Compile()(_categories[i])).Remove(itemToRemove);
            return Task.CompletedTask;
        }

        public Task ReplaceAsync(Category newItem)
        {
            var i = _categories.IndexOfFirst(x => x.Id == newItem.Id);
            _categories[i] = newItem;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(
            Category newItem, IEnumerable<Expression<Func<Category, object>>> propertiesToUpdate = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePropertyAsync<TValue>(
            ObjectId id, Expression<Func<Category, TValue>> propertyToUpdate, TValue value)
        {
            throw new NotImplementedException();
        }

        public async Task<BitmapImage> GetImageAsync(ObjectId categoryId)
        {
            return null;
        }

        public Task<ObjectId> UpdateImageAsync(ObjectId categoryId, Stream fileStream, string fileName)
        {
            return Task.FromResult(ObjectId.GenerateNewId());
        }
    }
}