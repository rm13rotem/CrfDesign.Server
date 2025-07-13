using BuisnessLogic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessLogic.Models
{
    public class CrfPageComponent : IPersistantEntity
    {
        public int Id { get; set; }
        public int CRFPageId { get; set; }  // Foreign key to CRFPage
        public string QuestionText { get; set; }
        public string RenderType { get; set; }
        public int QuestionTypeId { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool IsRequired { get; set; }

        public ICollection<CrfOption> Options { get; set; }  // For multiple-choice questions
        public string ValidationPattern { get; set; } // Optional for custom validation like regex

        // Navigation properties
        [JsonIgnoreAttribute]
        public CrfPage CrfPage { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public void FixByRenderType(DataContext.CrfDesignContext _context)
        {
            // figure out component type and fill fields
            if (RenderType == null)
                return;
            var QuestionType = _context.QuestionTypes.FirstOrDefault(x => x.Name.ToLower() == RenderType.ToLower());
            if (QuestionType == null && QuestionTypeId == 0)
            {
                QuestionType = _context.QuestionTypes
                    .FirstOrDefault(x => x.Name.ToLower() == "SingleChoice".ToLower());
                CategoryName = RenderType;
                var category = _context.CrfOptionCategories
                    .FirstOrDefault(x => x.Name == RenderType);
                if (category != null)
                    CategoryId = category.Id;
            }
        }

        public IPersistantEntity ToNewEntity()
        {
            CrfPageComponent result = new()
            {
                CRFPageId = CRFPageId,
                CategoryId = CategoryId,
                IsRequired = IsRequired,
                QuestionText = QuestionText,
                QuestionTypeId = QuestionTypeId,
                RenderType = RenderType,
                ValidationPattern = ValidationPattern,
                Name = this.Name,
                IsDeleted = this.IsDeleted,
                ModifiedDateTime = this.ModifiedDateTime
            };
            return result;
        }
    }
}
