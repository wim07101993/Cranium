using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Cranium.Data.RestClient.Attributes;
using Cranium.Data.RestClient.Models;
using Cranium.Data.RestClient.Models.Bases;
using Flurl;
using Flurl.Http;

namespace Cranium.Data.RestClient.Services
{
    public class Client : IClient
    {
        private readonly IClientSettings _clientSettings;


        public Client(IClientSettings clientSettings)
        {
            _clientSettings = clientSettings;
        }


        #region question 

        public async Task<IList<Question>> GetQuestionsAsync(int skip = 0, int take = -1)
        {
            throw new NotImplementedException();
        }

        public async Task<Question> GetQuestionAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateQuestionAsync(Question question)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteQuestionAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion question


        #region question type

        public async Task<IList<QuestionType>> GetQuestionTypesAsync(int skip = 0, int take = -1)
        {
            throw new NotImplementedException();
        }

        public async Task<QuestionType> GetQuestionTypeAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateQuestionTypeAsync(QuestionType questionType)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateQuestionTypeAsync(QuestionType questionType)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteQuestionTypeAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion question type


        #region category

        public async Task<IList<Category>> GetCategoriesAsync(int skip = 0, int take = -1)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> GetCategoryAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion category


        #region generic

        public async Task<IList<T>> GetAsync<T>(int skip = 0, int take = -1) where T : class, IWithId
        {
            return await $"{_clientSettings.HostName}/{GetControllerName<T>()}"
                .SetQueryParam("skip", skip)
                .SetQueryParam("take", take)
                .GetJsonAsync<List<T>>();
        }

        public async Task<T> GetAsync<T>(Guid id) where T : class, IWithId
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync<T>(T t) where T : class, IWithId
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync<T>(T t) where T : class, IWithId
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync<T>(Guid id) where T : class, IWithId
        {
            throw new NotImplementedException();
        }

        private string GetControllerName<T>()
        {
            var attribute = typeof(T).GetCustomAttribute<HasControllerAttribute>();
            if (attribute == null)
                throw new InvalidOperationException($"Cannot get controller from the model {typeof(T).Name}");

            return attribute.ControllerName;
        }

        #endregion generic
    }
}
