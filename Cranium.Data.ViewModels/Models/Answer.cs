using Cranium.Data.ViewModels.Models.Bases;

namespace Cranium.Data.ViewModels.Models
{
    public class Answer : AWithId
    {
        private bool _isCorrect;
        private string _value;
        private string _info;


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
    }
}