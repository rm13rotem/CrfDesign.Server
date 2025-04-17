using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models
{
    public class CrfPageComponent
    {
        public int Id { get; set; }
        public int CRFPageId { get; set; }  // Foreign key to CRFPage
        public string QuestionText { get; set; }
        public string RenderType { get; set; }
        public QuestionType QuestionType { get; set; }
        public bool IsRequired { get; set; }

        public ICollection<CrfOption> Options { get; set; }  // For multiple-choice questions
        public string ValidationPattern { get; set; } // Optional for custom validation like regex

        // Navigation properties
        public CrfPage CrfPage { get; set; }
    }
}
