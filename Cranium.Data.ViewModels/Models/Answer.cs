using Cranium.Data.ViewModels.Models.Bases;

namespace Cranium.Data.ViewModels.Models
{
    public class Answer : AWithId
    {
        private string _value;
        

        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}