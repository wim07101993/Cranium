using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Data.Models;

namespace Data.Services.Data.File
{
    public class DataService : ICategoryService, IQuestionService, IQuestionTypeService
    {
        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;
        private readonly IQuestionTypeService _questionTypeService;


        public DataService(ICategoryService categoryService, IQuestionService questionService, IQuestionTypeService questionTypeService)
        {
            _categoryService = categoryService;
            _questionService = questionService;
            _questionTypeService = questionTypeService;
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Category item)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Question item)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(QuestionType item)
        {
            throw new NotImplementedException();
        }

        Task<QuestionType> IDataService<QuestionType>.GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<QuestionType>> GetAsync(int skip = 0, int take = -1, DataRequest<QuestionType> dataRequest = null)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TOut>> SelectAsync<TOut>(Func<QuestionType, TOut> selector, int skip = 0, int take = -1, DataRequest<QuestionType> dataRequest = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(DataRequest<QuestionType> dataRequest = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(QuestionType item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(params QuestionType[] items)
        {
            throw new NotImplementedException();
        }

        Task<Question> IDataService<Question>.GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Question>> GetAsync(int skip = 0, int take = -1, DataRequest<Question> dataRequest = null)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TOut>> SelectAsync<TOut>(Func<Question, TOut> selector, int skip = 0, int take = -1, DataRequest<Question> dataRequest = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(DataRequest<Question> dataRequest = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Question item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(params Question[] items)
        {
            throw new NotImplementedException();
        }

        Task<Category> IDataService<Category>.GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Category>> GetAsync(int skip = 0, int take = -1, DataRequest<Category> dataRequest = null)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TOut>> SelectAsync<TOut>(Func<Category, TOut> selector, int skip = 0, int take = -1, DataRequest<Category> dataRequest = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(DataRequest<Category> dataRequest = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(params Category[] items)
        {
            throw new NotImplementedException();
        }
    }
}
