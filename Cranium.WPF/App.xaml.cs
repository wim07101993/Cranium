using System.Windows;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Mongo;
using Cranium.WPF.Services.Mongo.Implementations;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels;
using Cranium.WPF.ViewModels.Data;
using Cranium.WPF.ViewModels.Data.Implementations;
using Cranium.WPF.ViewModels.Implementations;
using Cranium.WPF.Views;
using Prism.Events;
using Unity;

namespace Cranium.WPF
{
    public partial class App
    {
        static App()
        {
            UnityContainer = new UnityContainer();

            RegisterTypes();
        }


        public static IUnityContainer UnityContainer { get; }

        public static Strings Strings => UnityContainer.Resolve<IStringsProvider>().Strings;


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = UnityContainer.Resolve<MainWindow>();
            if (MainWindow == null)
                MessageBox.Show("No window to show");
            else
                MainWindow.Show();
        }

        private static void RegisterTypes()
        {
            UnityContainer
                // services
                .RegisterInstance<IMongoDataServiceSettings>(new MongoDataServiceSettings{ConnectionString = "mongodb://localhost:27017", DatabaseName = "cranium"})
                .RegisterType<ICategoryService, CategoryService>()
                .RegisterType<IDataService<Category>, CategoryService>()
                .RegisterType<IQuestionTypeService, QuestionTypeService>()
                .RegisterType<IDataService<QuestionType>, QuestionTypeService>()
                .RegisterType<IQuestionService, QuestionService>()
                .RegisterType<IDataService<Question>, QuestionService>()
                .RegisterType<IFileService, FileService>()
                .RegisterSingleton<IStringsProvider, StringsProvider>()
                .RegisterSingleton<IEventAggregator, EventAggregator>()
                // view-models
                .RegisterType<IMainWindowViewModel, MainWindowViewModel>()
                .RegisterType<IDataViewModel, DataViewModel>()
                .RegisterType<IQuestionsViewModel, QuestionsViewModel>()
                .RegisterType<IQuestionViewModel, QuestionViewModel>()
                .RegisterType<IQuestionTypesViewModel, QuestionTypesViewModel>()
                .RegisterType<IQuestionTypeViewModel, QuestionTypeViewModel>()
                .RegisterType<ICategoriesViewModel, CategoriesViewModel>()
                .RegisterType<ICategoryViewModel, CategoryViewModel>()
                .RegisterType<IGameViewModel, GameViewModel>()
                .RegisterType<ISettingsViewModel, SettingsViewModel>()
                .RegisterSingleton<IHamburgerMenuViewModel, HamburgerMenuViewModel>()
                // extensions
                .AddExtension(new Diagnostic());
        }
    }
}