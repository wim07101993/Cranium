using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Cranium.Data.Models.Bases;

namespace Cranium.Data.Models
{
    [Table("Categories")]
    public class Category : AWithId
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string Color { get; set; }

        public ICollection<QuestionType> QuestionTypes { get; set; }
    }
}