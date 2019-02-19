using Prism.Mvvm;

namespace Cranium.WPF.Services.Strings
{
    public class Strings : BindableBase
    {
        #region fields

        private string _questions = "Vragen";
        private string _questionTypes = "Vraag types";
        private string _categories = "Categorieën";
        private string _game = "Spel";
        private string _data = "Data";
        private string _settings = "Instellingen";
        private string _cranium = "Cranium";

        #region data

        private string _name = "Naam";
        private string _task = "Opdracht";
        private string _tip = "Tip";
        private string _questionType = "Opdracht type";
        private string _category = "Categorie";
        private string _attachment = "Bijlage";
        private string _answers = "Antwoorden";
        private string _explanation = "Uitleg";
        private string _isCorrect = "Is juist";
        private string _value = "Antwoord";
        private string _info = "Info";
        private string _description = "Beschrijving";
        private string _image = "Afbeelding";
        private string _edit = "Aanpassen";
        private string _selectAnImageFile = "Selecteer een afbeelding";
        private string _color = "Kleur";
        private string _accept = "Accepteren";

        #endregion data

        #endregion fields

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

        #region data

        public string Task
        {
            get => _task;
            set => SetProperty(ref _task, value);
        }

        public string Tip
        {
            get => _tip;
            set => SetProperty(ref _tip, value);
        }

        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public string QuestionType
        {
            get => _questionType;
            set => SetProperty(ref _questionType, value);
        }

        public string Attachment
        {
            get => _attachment;
            set => SetProperty(ref _attachment, value);
        }

        public string Answers
        {
            get => _answers;
            set => SetProperty(ref _answers, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Explanation
        {
            get => _explanation;
            set => SetProperty(ref _explanation, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public string IsCorrect
        {
            get => _isCorrect;
            set => SetProperty(ref _isCorrect, value);
        }

        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public string Info
        {
            get => _info;
            set => SetProperty(ref _info, value);
        }

        public string Edit
        {
            get => _edit;
            set => SetProperty(ref _edit, value);
        }

        public string SelectAnImageFile
        {
            get => _selectAnImageFile;
            set => SetProperty(ref _selectAnImageFile, value);
        }

        public string Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        public string Accept
        {
            get => _accept;
            set => SetProperty(ref _accept, value);
        }

        #endregion data
    }
}