using Cranium.Data.Models.Bases;
using System.Collections.Generic;

namespace Cranium.Data.Models
{
    public class QuestionType : AWithId
    {
        public string Name { get; set; }

        public string Explanation { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}