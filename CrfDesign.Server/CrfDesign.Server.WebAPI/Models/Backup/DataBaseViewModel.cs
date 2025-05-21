using BuisnessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models.Backup
{
    public class DataBaseViewModel
    {
        public DataBaseViewModel(BuisnessLogic.DataContext.CrfDesignContext _context)
        {
            CrfOptionCategories = _context.CrfOptionCategories                .ToList();
            CrfOptions = _context.CrfOptions.ToList();
            CrfPages = _context.CrfPages.Select(x=>new CrfPageViewModel(x)).ToList();
            CrfPageComponents = _context.CrfPageComponents
                .Select(x => new CrfPageComponentViewModel(x, _context)).ToList();
            QuestionTypes = _context.QuestionTypes.ToList();
        }

        public List<CrfOptionCategory> CrfOptionCategories { get; set; }
        public List<BuisnessLogic.Models.CrfOption> CrfOptions { get; set; }
        public List<CrfPageViewModel> CrfPages { get; set; }
        public List<CrfPageComponentViewModel> CrfPageComponents { get; set; }
        public List<BuisnessLogic.Models.QuestionType> QuestionTypes { get; set; }
    }
}
