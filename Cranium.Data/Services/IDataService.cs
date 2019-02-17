using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cranium.Data.Models;
using Cranium.Data.Models.Bases;
using Microsoft.EntityFrameworkCore;

namespace Cranium.Data.Services
{
    public interface IDataService
    {
        #region question

        Task CreateQuestionAsync(Question item);

        Task<Question> GetQuestionAsync(Guid id);
        Task<IList<Question>> GetQuestionsAsync(int skip = 0, int take = -1, DataRequest<Question> dataRequest = null);

        Task<int> CountQuestionsAsync(DataRequest<Question> dataRequest = null);

        Task UpdateQuestionAsync(Question item);
        Task DeleteQuestionAsync(params Question[] items);

        #endregion question


        #region question-type

        Task CreateQuestionTypeAsync(QuestionType item);

        Task<QuestionType> GetQuestionTypeAsync(Guid id);

        Task<IList<QuestionType>> GetQuestionTypesAsync(
            int skip = 0, int take = -1, DataRequest<QuestionType> dataRequest = null);

        Task<int> CountQuestionTypesAsync(DataRequest<QuestionType> dataRequest = null);

        Task UpdateQuestionTypeAsync(QuestionType item);
        Task DeleteQuestionTypeAsync(params QuestionType[] items);

        #endregion question-type


        #region category

        Task CreateCategoryAsync(Category item);

        Task<Category> GetCategoryAsync(Guid id);
        Task<IList<Category>> GetCategoriesAsync(int skip = 0, int take = -1, DataRequest<Category> dataRequest = null);

        Task<int> CountCategoriesAsync(DataRequest<Category> dataRequest = null);

        Task UpdateCategoryAsync(Category item);
        Task DeleteCategoryAsync(params Category[] items);

        #endregion category


        #region generic

        Task CreateAsync<T>(Func<DataContext, DbSet<T>> set, T item) where T : class, IWithId;

        Task<T> GetAsync<T>(Func<DataContext, DbSet<T>> set, Guid id) where T : class, IWithId;

        Task<IList<T>> GetAsync<T>(
            Func<DataContext, DbSet<T>> set, int skip = 0, int take = -1, DataRequest<T> dataRequest = null)
            where T : class, IWithId;

        Task<int> CountAsync<T>(Func<DataContext, DbSet<T>> set, DataRequest<T> dataRequest = null)
            where T : class, IWithId;

        Task UpdateAsync<T>(Func<DataContext, DbSet<T>> set, T item) where T : class, IWithId;

        Task DeleteAsync<T>(Func<DataContext, DbSet<T>> set, params T[] items) where T : class, IWithId;

        #endregion generic
    }
}