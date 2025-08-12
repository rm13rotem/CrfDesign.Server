using BuisnessLogic.Models;
using BuisnessLogic.Repositories;
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
        public DataBaseViewModel(IInMemoryCrfDataStore _context)
        {
            // TODO - is this really neccessary? It's the same as InMemoryStore
            CrfOptionCategories = _context.CrfOptionCategories;
            CrfOptions = _context.CrfOptions;
            CrfPages = _context.CrfPages; // CrfPageComponents included here
            QuestionTypes = _context.QuestionTypes;
        }

        public List<CrfOptionCategory> CrfOptionCategories { get; set; }
        public List<CrfOption> CrfOptions { get; set; }
        public List<CrfPage> CrfPages { get; set; }
        public List<QuestionType> QuestionTypes { get; set; }
    }
}
