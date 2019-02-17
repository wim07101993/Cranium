using Prism.Mvvm;

namespace Cranium.WPF.Services.Strings
{
    public class Strings : BindableBase
    {
        private string _questions = "Vragen";
        private string _questionTypes = "Vraag types";
        private string _categories = "Categorieën";
        private string _game = "Spel";
        private string _data = "Data";
        private string _settings = "Instellingen";
        private string _cranium = "Cranium";


        public string Questions
        {
            get => _questions;
            set => SetProperty(ref _questions, value);
        }

        public string QuestionTypes
        {
            get => _questionTypes;
            set => SetProperty(ref _questionTypes, value);
        }

        public string Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        public string Game
        {
            get => _game;
            set => SetProperty(ref _game, value);
        }

        public string Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        public string Settings
        {
            get => _settings;
            set => SetProperty(ref _settings, value);
        }

        public string Cranium
        {
            get => _cranium;
            set => SetProperty(ref _cranium, value);
        }
    }
}