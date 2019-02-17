using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cranium.Data.RestClient.Models;
using Cranium.Data.RestClient.Models.Bases;

namespace Cranium.Data.RestClient.Services
{
    public interface IClient
    {
        #region question 

        Task<IList<Question>> GetQuestionsAsync(int skip = 0, int take = -1);
        Task<Question> GetQuestionAsync(Guid id);

        Task CreateQuestionAsync(Question question);
        Task UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(Guid id);

        #endregion question


        #region question type

        Task<IList<QuestionType>> GetQuestionTypesAsync(int skip = 0, int take = -1);
        Task<QuestionType> GetQuestionTypeAsync(Guid id);

        Task CreateQuestionTypeAsync(QuestionType questionType);
        Task UpdateQuestionTypeAsync(QuestionType questionType);
        Task DeleteQuestionTypeAsync(Guid id);

        #endregion question type


        #region category

        Task<IList<Category>> GetCategoriesAsync(int skip = 0, int take = -1);
        Task<Category> GetCategoryAsync(Guid id);

        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Guid id);

        #endregion category


        #region generic

        Task<IList<T>> GetAsync<T>(int skip = 0, int take = -1) where T : class, IWithId;
        Task<T> GetAsync<T>(Guid id) where T : class, IWithId;

        Task CreateAsync<T>(T t) where T : class, IWithId;
        Task UpdateAsync<T>(T t) where T : class, IWithId;
        Task DeleteAsync<T>(Guid id) where T : class, IWithId;

        #endregion generic
    }
}