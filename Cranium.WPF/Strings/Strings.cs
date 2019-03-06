using Prism.Mvvm;

namespace Cranium.WPF.Strings
{
    public class Strings : BindableBase
    {
        #region FIELDS

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
        private string _required = "Verplicht";
        private string _more = "Meer";
        private string _file = "Bestand";
        private string _selectAnAttachment = "Selecteer een bijlage";

        #endregion data

        #region game

        private string _correct = "Juist";
        private string _inCorrect = "Fout";
        private string _newPlayer = "Nieuw team";
        private string _players = "Teams";
        private string _question = "Opdracht";
        private string _createGame = "Create game";
        private string _gameTimeInMinutes = "Game-time (min)";
        private string _selectACategory = "Selecteer een categorie";
        private string _moveBackwards = "Achteruit gaan";

        #endregion game

        #endregion FIELDS


        #region PROPERTIES

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

        public string Required
        {
            get => _required;
            set => SetProperty(ref _required, value);
        }

        public string More
        {
            get => _more;
            set => SetProperty(ref _more, value);
        }

        public string File
        {
            get => _file;
            set => SetProperty(ref _file, value);
        }

        public string SelectAnAttachment
        {
            get => _selectAnAttachment;
            set => SetProperty(ref _selectAnAttachment, value);
        }

        #endregion data

        #region game

        public string Correct
        {
            get => _correct;
            set => SetProperty(ref _correct, value);
        }

        public string InCorrect
        {
            get => _inCorrect;
            set => SetProperty(ref _inCorrect, value);
        }

        public string NewPlayer
        {
            get => _newPlayer;
            set => SetProperty(ref _newPlayer, value);
        }

        public string Players
        {
            get => _players;
            set => SetProperty(ref _players, value);
        }

        public string Question
        {
            get => _question;
            set => SetProperty(ref _question, value);
        }

        public string CreateGame
        {
            get => _createGame;
            set => SetProperty(ref _createGame, value);
        }

        public string GameTimeInMinutes
        {
            get => _gameTimeInMinutes;
            set => SetProperty(ref _gameTimeInMinutes, value);
        }

        public string SelectACategory
        {
            get => _selectACategory;
            set => SetProperty(ref _selectACategory, value);
        }

        public string MoveBackwards
        {
            get => _moveBackwards;
            set => SetProperty(ref _moveBackwards, value);
        }

        #endregion game

        #endregion PROPERTIES
    }
}