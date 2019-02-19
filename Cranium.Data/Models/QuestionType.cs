using Cranium.Data.Models.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cranium.Data.Models
{
    public class QuestionType : AWithId
    {
        public string Name { get; set; }

        public string Explanation { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }


        public ICollection<Question> Questions { get; set; }
    }
}