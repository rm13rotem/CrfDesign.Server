using Newtonsoft.Json;
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

        public BuisnessLogic.Models.CrfPageComponent FixAccordingToRenderType(BuisnessLogic.DataContext.CrfDesignContext _context)
        {
            var dbEntity = new BuisnessLogic.Models.CrfPageComponent();
            string jsonString = JsonConvert.SerializeObject(this);
            dbEntity = JsonConvert.DeserializeObject<BuisnessLogic.Models.CrfPageComponent>(jsonString);
            dbEntity.QuestionType = _context.QuestionTypes.FirstOrDefault(x => x.Name == RenderType);
            if (dbEntity.QuestionType == null)
            {
                switch (RenderType.Substring(0, Math.Min(5, RenderType.Length)))
                {
                    case "yesno":
                        dbEntity.QuestionType = _context.QuestionTypes.FirstOrDefault(x => x.Name == "SingleChoise");
                        dbEntity.Options = _context.CrfOptions.Where(x => x.CrfQuestionId == 1).ToArray();

                        break;

                    default:
                        break;
                }
            }

            return dbEntity;
        }
    }
}
