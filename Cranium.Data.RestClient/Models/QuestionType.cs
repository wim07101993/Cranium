using Cranium.Data.RestClient.Attributes;
using Cranium.Data.RestClient.Models.Bases;

namespace Cranium.Data.RestClient.Models
{
    [HasController]
    public class QuestionType : AWithId
    {
        private string _name;
        private string _explanation;


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
    }
}