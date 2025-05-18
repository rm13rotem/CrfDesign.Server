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
            CRFPageName = component.CrfPage.Name;
            QuestionText = component.QuestionText;
            RenderType = component.RenderType;
            QuestionType = component.QuestionType?.Name;
            IsRequired = component.IsRequired;
            CategoryId = component.CategoryId ?? 0;
            CategoryName = component.CategoryName;
            ValidationPattern = component.ValidationPattern;        
    }
        public string ToCategoryOptions()
        {
            if (CategoryId != 0)
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
        public string QuestionType { get; set; }
        public bool IsRequired { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ValidationPattern { get; set; }

        public string CategoryOptions { get { return ToCategoryOptions(); } }

    }
}
