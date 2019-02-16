using System.Collections.ObjectModel;
using Data.Models.Bases;

namespace Data.Models
{
    public class Question : AWithId
    {
        #region FIELDS

        private string _task;
        private string _answer;
        private string _tip;

        private Category _category;
        private QuestionType _questionType;

        private byte[] _attachment;

        private ObservableCollection<string> _possibleAnswers;

        #endregion FIELDS


        #region PROPERTIES

        public string Task
        {
            get => _task;
            set => SetProperty(ref _task, value);
        }

        public string Answer
        {
            get => _answer;
            set => SetProperty(ref _answer, value);
        }

        public string Tip
        {
            get => _tip;
            set => SetProperty(ref _tip, value);
        }

        public Category Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public QuestionType QuestionType
        {
            get => _questionType;
            set => SetProperty(ref _questionType, value);
        }

        public byte[] Attachment
        {
            get => _attachment;
            set => SetProperty(ref _attachment, value);
        }

        public ObservableCollection<string> PossibleAnswers
        {
            get => _possibleAnswers;
            set => SetProperty(ref _possibleAnswers, value);
        }

        #endregion PROPERTIES
    }
}