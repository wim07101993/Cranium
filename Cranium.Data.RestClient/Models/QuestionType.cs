using System;
using Cranium.Data.RestClient.Attributes;
using Cranium.Data.RestClient.Models.Bases;
using Newtonsoft.Json;

namespace Cranium.Data.RestClient.Models
{
    [HasController(nameof(QuestionType))]
    public class QuestionType : AWithId
    {
        private string _name;
        private string _explanation;
        private Category _category;
        private Guid _categoryId;


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

        public Guid CategoryId
        {
            get => _categoryId;
            set => SetProperty(ref _categoryId, value);
        }

        [JsonIgnore]
        public Category Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }
    }
}