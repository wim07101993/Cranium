using System.Windows;
using Cranium.Data.RestClient;
using Cranium.Data.RestClient.Services;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels;
using Cranium.WPF.ViewModels.Implementations;
using Cranium.WPF.Views;
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
                .RegisterInstance<IClientSettings>(new ClientSettings{HostName = "http://localhost:5000/api"})
                .RegisterType<IClient, Client>()
//                .RegisterType<IClient, MockDataService>()
                .RegisterSingleton<IStringsProvider, StringsProvider>()
                // view-models
                .RegisterType<IMainWindowViewModel, MainWindowViewModel>()
                .RegisterType<IDataViewModel, DataViewModel>()
                .RegisterSingleton<IQuestionsViewModel, QuestionsViewModel>()
                .RegisterSingleton<IQuestionTypesViewModel, QuestionTypesViewModel>()
                .RegisterSingleton<ICategoriesViewModel, CategoriesViewModel>()
                .RegisterType<IGameViewModel, GameViewModel>()
                .RegisterType<ISettingsViewModel, SettingsViewModel>()
                .RegisterSingleton<IHamburgerMenuViewModel, HamburgerMenuViewModel>();
        }
    }
}