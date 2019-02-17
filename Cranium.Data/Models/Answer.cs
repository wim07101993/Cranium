using System;
using System.ComponentModel.DataAnnotations.Schema;
using Cranium.Data.Models.Bases;

namespace Cranium.Data.Models
{
    public class Answer : AWithId
    {
        public Guid QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }

        public bool IsCorrect { get; set; }

        public string Value { get; set; }

        public string Info { get; set; }
    }
}