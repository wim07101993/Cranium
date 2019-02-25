using Cranium.WPF.Services.Mongo;
using System;
using System.Threading.Tasks;

namespace Cranium.WPF.Services.Game
{
    public class GameService
    {
        #region FIELDS

        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;

        #endregion FIELDS


        #region CONSTRUCTORS

        public GameService(ICategoryService categoryService, IQuestionService questionService)
        {
            _categoryService = categoryService;
            _questionService = questionService;
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES



        #endregion PROPERTIES


        #region METHODS
        
        public async Task<Game> CreateGame()
        {
            throw new NotImplementedException();
        }
        
        #endregion METHODS
    }
}
