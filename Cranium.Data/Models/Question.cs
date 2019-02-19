using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Cranium.Data.Models.Bases;

namespace Cranium.Data.Models
{
    public class Question : AWithId
    {
        public string Task { get; set; }
        
        public string Tip { get; set; }

        public Guid QuestionTypeId { get; set; }

        [ForeignKey(nameof(QuestionTypeId))]
        public QuestionType QuestionType { get; set; }

        public byte[] Attachment { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}