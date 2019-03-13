using Cranium.WPF.Data.Category;
using Cranium.WPF.Data.Files;
using Cranium.WPF.Data.Game;
using Cranium.WPF.Data.Question;
using Cranium.WPF.Data.QuestionType;
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
                .RegisterType<ICategoryService, MongoCategoryService>()
                .RegisterType<IModelService<Category>, MongoCategoryService>()
                .RegisterType<IQuestionTypeService, MongoQuestionTypeService>()
                .RegisterType<IModelService<QuestionType>, MongoQuestionTypeService>()
                .RegisterType<IQuestionService, MongoQuestionService>()
                .RegisterType<IModelService<Question>, MongoQuestionService>()
                .RegisterType<IAttachmentService, MongoAttachmentService>()
                .RegisterType<IGameDataService, MongoGameService>();

        public static IUnityContainer RegisterFileDataServices(this IUnityContainer container)
            => container
                .RegisterType<ICategoryService, FileCategoryService>()
                .RegisterType<IModelService<Category>, FileCategoryService>()
                .RegisterType<IQuestionTypeService, FileQuestionTypeService>()
                .RegisterType<IModelService<QuestionType>, FileQuestionTypeService>()
                .RegisterType<IQuestionService, FileQuestionService>()
                .RegisterType<IModelService<Question>, FileQuestionService>()
                .RegisterType<IAttachmentService, FileAttachmentService>()
                .RegisterType<IGameDataService, FileGameService>();
    }
}
