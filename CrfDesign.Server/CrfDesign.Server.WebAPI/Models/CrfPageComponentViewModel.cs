using BuisnessLogic.DataContext;
using BuisnessLogic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models
{
    public class CrfPageComponentViewModel
    {
        public CrfPageComponentViewModel(CrfPageComponent component, CrfDesignContext context)
        {
            _context = context;
            Id = component.Id;
            var crfPage = _context.CrfPages.FirstOrDefault(x => x.Id == component.CRFPageId);
            if (crfPage != null)
                CRFPageName = crfPage.Id.ToString() + " - " + crfPage.Name;
            QuestionText = component.QuestionText;
            RenderType = component.RenderType;
            QuestionTypeId = component.QuestionTypeId;
            var _questionType = _context.QuestionTypes.FirstOrDefault(x => x.Id == component.QuestionTypeId);
            QuestionType = _questionType?.Name ?? string.Empty;
            IsRequired = component.IsRequired;
            CategoryId = component.CategoryId ?? 0;
            if (CategoryId > 0)
            {
                var category = _context.CrfOptionCategories.FirstOrDefault(x => x.Id == CategoryId);
                if (category != null)
                    CategoryName = category.Name;
            }
            ValidationPattern = component.ValidationPattern;
            Name = component.Name;
        }

        public string ToCategoryOptions()
        {
            if (CategoryId > 0)
            {
                var CategoryOptions = _context.CrfOptions.Where(x => x.CrfOptionCategoryId == CategoryId);
                if (CategoryOptions.Any())
                {
                    var result = string.Empty;
                    foreach (var categoryOption in CategoryOptions)
                    {
                        result += categoryOption.Name + "\\";
                    }
                    return result.Substring(0, result.Length - 1);
                }
                else return CategoryName + "**";

            }

            return string.Empty;
        }
        public CrfDesignContext _context { get; set; }
        public int Id { get; set; }
        public string CRFPageName { get; set; }  // From CRFPage
        public string QuestionText { get; set; }
        public string RenderType { get; set; }
        public int QuestionTypeId { get; private set; }
        public string QuestionType { get; set; }
        public bool IsRequired { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ValidationPattern { get; set; }
        public string Name { get; private set; }

        public string CategoryOptions { get { return ToCategoryOptions(); } }

    }
}
