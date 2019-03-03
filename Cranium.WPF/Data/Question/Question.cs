using System.Collections.ObjectModel;
using Cranium.WPF.Data.Files;
using Cranium.WPF.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cranium.WPF.Data.Question
{
    public class Question : AWithId
    {
        private string _task;
        private string _tip;
        private QuestionType.QuestionType _questionType;
        private ObjectId _attachment;
        private ObservableCollection<Answer.Answer> _answers;
        private EAttachmentType _attachmentType;

        [BsonElement("task")]
        public string Task
        {
            get => _task;
            set => SetProperty(ref _task, value);
        }

        [BsonElement("tip")]
        public string Tip
        {
            get => _tip;
            set => SetProperty(ref _tip, value);
        }

        [BsonRequired]
        [BsonElement("questionType")]
        public QuestionType.QuestionType QuestionType
        {
            get => _questionType;
            set => SetProperty(ref _questionType, value);
        }

        [BsonElement("attachment")]
        public ObjectId Attachment
        {
            get => _attachment;
            set => SetProperty(ref _attachment, value);
        }

        [BsonElement("attachmentType")]
        public EAttachmentType AttachmentType
        {
            get => _attachmentType;
            set => SetProperty(ref _attachmentType, value);
        }

        [BsonRequired]
        [BsonElement("answers")]
        public ObservableCollection<Answer.Answer> Answers
        {
            get => _answers;
            set => SetProperty(ref _answers, value);
        }
    }
}