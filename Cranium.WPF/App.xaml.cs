﻿using System.Windows;
using Cranium.WPF.Data;
using Cranium.WPF.Data.Answer;
using Cranium.WPF.Data.Category;
using Cranium.WPF.Data.Files;
using Cranium.WPF.Data.Question;
using Cranium.WPF.Data.QuestionType;
using Cranium.WPF.Game;
using Cranium.WPF.Game.Control;
using Cranium.WPF.Game.GameBoard;
using Cranium.WPF.Game.Player;
using Cranium.WPF.Game.Tile;
using Cranium.WPF.HamburgerMenu;
using Cranium.WPF.Helpers.Mongo;
using Cranium.WPF.Settings;
using Cranium.WPF.Strings;
using Prism.Events;
using Unity;
using QuestionViewModel = Cranium.WPF.Data.Question.QuestionViewModel;

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

        public static Strings.Strings Strings => UnityContainer.Resolve<IStringsProvider>().Strings;

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
                .RegisterType< MainWindowViewModel>()
                // data
                .RegisterType<DataViewModel>()
                .RegisterType<QuestionsViewModel>()
                .RegisterType<QuestionViewModel>()
                .RegisterType<QuestionTypesViewModel>()
                .RegisterType<QuestionTypeViewModel>()
                .RegisterType<AnswersViewModel>()
                .RegisterType<AnswerViewModel>()
                .RegisterType<CategoriesViewModel>()
                .RegisterType<CategoryViewModel>()
                // game
                .RegisterType< GameViewModel>()
                .RegisterType< GameBoardViewModel>()
                .RegisterType< TileViewModel>()
                .RegisterType< Game.Question.QuestionViewModel>()
                .RegisterType< ControlWindowViewModel>()
                .RegisterType< PlayerViewModel>()
                .RegisterType< PlayerViewModel>()
                // settings
                .RegisterType< SettingsViewModel>()
                .RegisterSingleton< HamburgerMenuViewModel>()
                // extensions
                .AddExtension(new Diagnostic());
        }
    }
}