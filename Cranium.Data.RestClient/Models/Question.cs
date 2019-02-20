using System;
using System.Collections.ObjectModel;
using Cranium.Data.RestClient.Attributes;
using Cranium.Data.RestClient.Models.Bases;
using Newtonsoft.Json;

namespace Cranium.Data.RestClient.Models
{
    [HasController(nameof(Question))]
    public class Question : AWithId
    {
        #region FIELDS

        private string _task;
        private string _tip;
        private QuestionType _questionType;
        private Guid _questionTypeId;
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
        
        [JsonIgnore]
        public QuestionType QuestionType
        {
            get => _questionType;
            set => SetProperty(ref _questionType, value);
        }

        public Guid QuestionTypeId
        {
            get => _questionTypeId;
            set => SetProperty(ref _questionTypeId, value);
        }

        public byte[] Attachment
        {
            get => _attachment;
            set => SetProperty(ref _attachment, value);
        }

        public ObservableCollection<Answer> Answers
        {
            get => _answers ?? (_answers = new ObservableCollection<Answer>());
            set => SetProperty(ref _answers, value);
        }

        #endregion PROPERTIES
    }
}