using BuisnessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models.Backup
{
    public class DataBaseViewModel
    {
        public DataBaseViewModel()
        {

        }
        public DataBaseViewModel(BuisnessLogic.DataContext.CrfDesignContext _context)
        {
            CrfOptionCategories = _context.CrfOptionCategories.ToList();
            CrfOptions = _context.CrfOptions.ToList();
            CrfPages = _context.CrfPages.ToList();
            CrfPageComponents = _context.CrfPageComponents.ToList();
            QuestionTypes = _context.QuestionTypes.ToList();
        }

        public List<CrfOptionCategory> CrfOptionCategories { get; set; }
        public List<CrfOption> CrfOptions { get; set; }
        public List<CrfPage> CrfPages { get; set; }
        public List<CrfPageComponent> CrfPageComponents { get; set; }
        public List<QuestionType> QuestionTypes { get; set; }
    }
}
