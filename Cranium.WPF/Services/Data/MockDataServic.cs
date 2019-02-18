using Cranium.Data.RestClient.Exceptions;
using Cranium.Data.RestClient.Models;
using Cranium.Data.RestClient.Models.Bases;
using Cranium.Data.RestClient.Services;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cranium.WPF.Services.Data
{
    public class MockDataService : IClient
    {
        public IList<Question> Questions { get; } = new List<Question>();
        public IList<QuestionType> QuestionTypes { get; } = new List<QuestionType>();
        public IList<Category> Categories { get; } = new List<Category>();


        public Task<T> CreateAsync<T>(T t) where T : class, IWithId
        {
            t.Id = Guid.NewGuid();
            GetList<T>().Add(t);
            return Task.FromResult(t);
        }

        public Task<Guid> DeleteAsync<T>(Guid id) where T : class, IWithId
        {
            GetList<T>().RemoveFirst(x => x.Id == id);
            return Task.FromResult(id);
        }

        public Task<IList<T>> GetAsync<T>(int skip, int take) where T : class, IWithId
        {
            return Task.FromResult(GetList<T>());
        }

        public Task<T> GetAsync<T>(Guid id) where T : class, IWithId
        {
            try
            {
                var t = GetList<T>().Single(x => x.Id == id);
                return Task.FromResult(t);
            }
            catch (InvalidOperationException)
            {
                throw new Http400Exception($"Could not find {typeof(T).Name} with id {id}");
            }
        }

        public Task<T> UpdateAsync<T>(T t) where T : class, IWithId
        {
            var list = GetList<T>();
            var i = list.IndexOfFirst(x => x.Id == t.Id);
            if (i < 0)
                throw new Http400Exception($"Could not find {typeof(T).Name} with id {t.Id}");

            list[i] = t;
            return Task.FromResult(list[i]);
        }

        private IList<T> GetList<T>()
        {
            IList<T> list = null;

            if (typeof(T) == typeof(Question))
                list = Questions as IList<T>;
            if (typeof(T) == typeof(QuestionType))
                list = QuestionTypes as IList<T>;
            if (typeof(T) == typeof(Category))
                list = Categories as IList<T>;

            return list ?? throw new InvalidOperationException($"The type {typeof(T).Name} is not supported");
        }
    }
}
