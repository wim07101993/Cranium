using Cranium.WPF.Data.Files;
using Cranium.WPF.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cranium.WPF.Data.Answer
{
    public class Answer : AWithId
    {
        private bool _isCorrect;
        private string _value;
        private string _info;
        private ObjectId _attachment;
        private EAttachmentType _attachmentType;


        [BsonRequired]
        [BsonElement("isCorrect")]
        public bool IsCorrect
        {
            get => _isCorrect;
            set => SetProperty(ref _isCorrect, value);
        }

        [BsonRequired]
        [BsonElement("value")]
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        [BsonElement("info")]
        public string Info
        {
            get => _info;
            set => SetProperty(ref _info, value);
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

    }
}