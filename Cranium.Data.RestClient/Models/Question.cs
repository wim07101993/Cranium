using System.Collections.ObjectModel;
using Cranium.Data.RestClient.Attributes;
using Cranium.Data.RestClient.Models.Bases;

namespace Cranium.Data.RestClient.Models
{
    [HasController]
    public class Question : AWithId
    {
        #region FIELDS

        private string _task;
        private string _tip;
        private QuestionType _questionType;
        private byte[] _attachment;
        private ObservableCollection<Answer> _answers;

        #endregion FIELDS


        #region PROPERTIES

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

        public ObservableCollection<Answer> Answers
        {
            get => _answers;
            set => SetProperty(ref _answers, value);
        }

        #endregion PROPERTIES
    }
}