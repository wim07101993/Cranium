﻿using System.Windows;
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
        public App()
        {
            UnityContainer = new UnityContainer();
        }


        public IUnityContainer UnityContainer { get; }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            RegisterTypes();

            MainWindow = UnityContainer.Resolve<MainWindow>();
            if (MainWindow == null)
                MessageBox.Show("No window to show");
            else
                MainWindow.Show();
        }

        private void RegisterTypes()
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
                .RegisterType<IQuestionsViewModel, QuestionsViewModel>()
                .RegisterType<IQuestionTypesViewModel, QuestionTypesViewModel>()
                .RegisterType<ICategoriesViewModel, CategoriesViewModel>()
                .RegisterType<IGameViewModel, GameViewModel>()
                .RegisterType<ISettingsViewModel, SettingsViewModel>()
                .RegisterSingleton<IHamburgerMenuViewModel, HamburgerMenuViewModel>();
        }
    }
}