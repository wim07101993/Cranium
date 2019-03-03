using Cranium.WPF.Helpers;
using MongoDB.Bson.Serialization.Attributes;

namespace Cranium.WPF.Data.QuestionType
{
    public class QuestionType : AWithId
    {
        private string _name;
        private string _explanation;
        private Category.Category _category;

        [BsonRequired]
        [BsonElement("name")]
        public string Name
        {
            get => _name;
            set => SetProperty( ref _name, value);
        }

        [BsonElement("explanation")]
        public string Explanation
        {
            get => _explanation;
            set => SetProperty( ref _explanation , value);
        }

        [BsonRequired]
        [BsonElement("category")]
        public Category.Category Category
        {
            get => _category;
            set => SetProperty( ref _category , value);
        }

        public override string ToString() => Name;
    }
}