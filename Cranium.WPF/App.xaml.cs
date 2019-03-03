using System.Windows;
using Cranium.WPF.Models;
using Cranium.WPF.Services;
using Cranium.WPF.Services.Game;
using Cranium.WPF.Services.Mongo;
using Cranium.WPF.Services.Mongo.Implementations;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels;
using Cranium.WPF.ViewModels.Data;
using Cranium.WPF.ViewModels.Data.Implementations;
using Cranium.WPF.ViewModels.Game;
using Cranium.WPF.ViewModels.Game.Implementations;
using Cranium.WPF.ViewModels.Implementations;
using Cranium.WPF.Views;
using Cranium.WPF.Views.Game;
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

        public ControlWindow ControlWindow { get; set; }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = UnityContainer.Resolve<MainWindow>();
            if (MainWindow == null)
            {
                MessageBox.Show("No window to show");
                return;
            }

            ControlWindow = UnityContainer.Resolve<ControlWindow>();

            MainWindow.Show();
            ControlWindow.Show();
        }

        private static void RegisterTypes()
        {
            UnityContainer
                // SERVICES
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
                .RegisterType<IFileService, FileService>()
                .RegisterSingleton<IStringsProvider, StringsProvider>()
                .RegisterSingleton<IEventAggregator, EventAggregator>()
                .RegisterSingleton<IGameService, GameService>()
                .RegisterType<IModelService<Player>, PlayerService>()
                .RegisterType<IPlayerService, PlayerService>()
                // VIEW-MODELS
                .RegisterType<IMainWindowViewModel, MainWindowViewModel>()
                // data
                .RegisterType<IDataViewModel, DataViewModel>()
                .RegisterType<IQuestionsViewModel, QuestionsViewModel>()
                .RegisterType<ViewModels.Data.IQuestionViewModel, ViewModels.Data.Implementations.QuestionViewModel>()
                .RegisterType<IQuestionTypesViewModel, QuestionTypesViewModel>()
                .RegisterType<IQuestionTypeViewModel, QuestionTypeViewModel>()
                .RegisterType<IAnswersViewModel, AnswersViewModel>()
                .RegisterType<IAnswerViewModel, AnswerViewModel>()
                .RegisterType<ICategoriesViewModel, CategoriesViewModel>()
                .RegisterType<ICategoryViewModel, CategoryViewModel>()
                // game
                .RegisterType<IGameViewModel, GameViewModel>()
                .RegisterType<IGameBoardViewModel, GameBoardViewModel>()
                .RegisterType<ITileViewModel, TileViewModel>()
                .RegisterType<ViewModels.Game.IQuestionViewModel, ViewModels.Game.Implementations.QuestionViewModel>()
                .RegisterType<IControlWindowViewModel, ControlWindowViewModel>()
                .RegisterType<IModelContainer<Player>, PlayerViewModel>()
                .RegisterType<IPlayerViewModel, PlayerViewModel>()
                // settings
                .RegisterType<ISettingsViewModel, SettingsViewModel>()
                .RegisterSingleton<IHamburgerMenuViewModel, HamburgerMenuViewModel>()
                // extensions
                .AddExtension(new Diagnostic());
        }
    }
}