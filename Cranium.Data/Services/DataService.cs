using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cranium.Data.Models;
using Cranium.Data.Models.Bases;
using Microsoft.EntityFrameworkCore;

namespace Cranium.Data.Services
{
    public class DataService : IDataService
    {
        private readonly DataContext _context;


        public DataService(DataContext context)
        {
            _context = context;
        }


        #region question

        public async Task CreateQuestionAsync(Question item)
            => await CreateAsync(x => x.Questions, item);

        public async Task<Question> GetQuestionAsync(Guid id)
            => await GetAsync(x => x.Questions, id);

        public async Task<IList<Question>> GetQuestionsAsync(
            int skip = 0, int take = -1, DataRequest<Question> dataRequest = null)
            => await GetAsync(x => x.Questions, skip, take, dataRequest);

        public async Task<int> CountQuestionsAsync(DataRequest<Question> dataRequest = null)
            => await CountAsync(x => x.Questions, dataRequest);

        public async Task UpdateQuestionAsync(Question item)
            => await UpdateAsync(x => x.Questions, item);

        public async Task DeleteQuestionAsync(params Question[] items)
            => await DeleteAsync(x => x.Questions, items);

        #endregion question


        #region question-type

        public async Task CreateQuestionTypeAsync(QuestionType item)
            => await CreateAsync(x => x.QuestionTypes, item);

        public async Task<QuestionType> GetQuestionTypeAsync(Guid id)
            => await GetAsync(x => x.QuestionTypes, id);

        public async Task<IList<QuestionType>> GetQuestionTypesAsync(
            int skip = 0, int take = -1, DataRequest<QuestionType> dataRequest = null)
            => await GetAsync(x => x.QuestionTypes, skip, take, dataRequest);

        public async Task<int> CountQuestionTypesAsync(DataRequest<QuestionType> dataRequest = null)
            => await CountAsync(x => x.QuestionTypes, dataRequest);

        public async Task UpdateQuestionTypeAsync(QuestionType item)
            => await UpdateAsync(x => x.QuestionTypes, item);

        public async Task DeleteQuestionTypeAsync(params QuestionType[] items)
            => await DeleteAsync(x => x.QuestionTypes, items);

        #endregion question-type


        #region category

        public async Task CreateCategoryAsync(Category item)
            => await CreateAsync(x => x.Categories, item);

        public async Task<Category> GetCategoryAsync(Guid id)
            => await GetAsync(x => x.Categories, id);

        public async Task<IList<Category>> GetCategoriesAsync(
            int skip = 0, int take = -1, DataRequest<Category> dataRequest = null)
            => await GetAsync(x => x.Categories, skip, take, dataRequest);

        public async Task<int> CountCategoriesAsync(DataRequest<Category> dataRequest = null)
            => await CountAsync(x => x.Categories, dataRequest);

        public async Task UpdateCategoryAsync(Category item)
            => await UpdateAsync(x => x.Categories, item);

        public async Task DeleteCategoryAsync(params Category[] items)
            => await DeleteAsync(x => x.Categories, items);

        #endregion category


        #region generic

        public async Task CreateAsync<T>(Func<DataContext, DbSet<T>> set, T item) where T : class, IWithId
        {
            item.Id = Guid.NewGuid();
            set(_context).Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetAsync<T>(Func<DataContext, DbSet<T>> set, Guid id) where T : class, IWithId
            => await set(_context).FirstAsync(x => x.Id == id);

        public async Task<IList<T>> GetAsync<T>(
            Func<DataContext, DbSet<T>> set, int skip = 0, int take = -1, DataRequest<T> dataRequest = null)
            where T : class, IWithId
        {
            var query = set(_context).AsQueryable();

            if (dataRequest?.Where != null)
                query = query.Where(dataRequest.Where);
            if (dataRequest?.OrderBy != null)
                query = query.OrderBy(dataRequest.OrderBy);
            if (dataRequest?.OrderByDescending != null)
                query = query.OrderByDescending(dataRequest.OrderByDescending);

            return await query.ToListAsync();
        }

        public async Task<int> CountAsync<T>(Func<DataContext, DbSet<T>> set, DataRequest<T> dataRequest = null)
            where T : class, IWithId
        {
            var query = set(_context).AsQueryable();

            if (dataRequest?.Where != null)
                query = query.Where(dataRequest.Where);

            return await query.CountAsync();
        }

        public async Task UpdateAsync<T>(Func<DataContext, DbSet<T>> set, T item) where T : class, IWithId
        {
            set(_context).Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(Func<DataContext, DbSet<T>> set, params T[] items) where T : class, IWithId
        {
            set(_context).RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        #endregion generic
    }
}