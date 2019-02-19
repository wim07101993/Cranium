using Cranium.Data.RestClient.Models.Bases;

namespace Cranium.Data.RestClient.Models
{
    public class Answer : AWithId
    {
        private bool _isCorrect;
        private string _value;
        private string _info;
        private byte[] _attachment;


        public bool IsCorrect
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

        public byte[] Attachment
        {
            get => _attachment;
            set => SetProperty(ref _attachment, value);
        }
    }
}