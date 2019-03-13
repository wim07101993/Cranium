using Cranium.WPF.Data.Category;
using Cranium.WPF.Data.Files;
using Cranium.WPF.Data.Question;
using Cranium.WPF.Data.QuestionType;
using Cranium.WPF.Game;
using Cranium.WPF.Helpers.Data;
using Cranium.WPF.Helpers.Data.Mongo;
using Unity;

namespace Cranium.WPF.Helpers.Extensions
{
    public static class UnityContainerExtensions
    {
        public static IUnityContainer RegisterMongoDataServices(this IUnityContainer container) 
            => container
                .RegisterInstance<IMongoDataServiceSettings>(new MongoDataServiceSettings
                {
                    ConnectionString = "mongodb://localhost:27017",
                    DatabaseName = "cranium"
                })
                .RegisterType<ICategoryService, CategoryService>()
                .RegisterType<IModelService<Category>, CategoryService>()
                .RegisterType<IQuestionTypeService, QuestionTypeService>()
                .RegisterType<IModelService<QuestionType>, QuestionTypeService>()
                .RegisterType<IQuestionService, QuestionService>()
                .RegisterType<IModelService<Question>, QuestionService>()
                .RegisterType<IFileService, MongoFileService>()
                .RegisterType<IGameDataService, MongoGameService>();

        public static IUnityContainer RegisterFileDataServices(this IUnityContainer container)
            => container
                .RegisterType<ICategoryService, CategoryFileService>()
                .RegisterType<IModelService<Category>, CategoryFileService>()
                .RegisterType<IQuestionTypeService, QuestionTypeFileService>()
                .RegisterType<IModelService<QuestionType>, QuestionTypeFileService>()
                .RegisterType<IQuestionService, QuestionFileService>()
                .RegisterType<IModelService<Question>, QuestionFileService>()
                .RegisterType<IFileService, MediaFileService>()
                .RegisterType<IGameDataService, FileGameService>();
    }
}
