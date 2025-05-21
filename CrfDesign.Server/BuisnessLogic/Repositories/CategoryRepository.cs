using BuisnessLogic.DataContext;
using BuisnessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.Repositories
{
    public class CategoryRepository
    {
        private static CategoryRepository _repository;
        public static CrfDesignContext DB;

        public static CategoryRepository Instance
        {
            get
            {
                if (DB == null)
                    return null;
                _repository = new CategoryRepository();
                return _repository;
            }
        }
        public void AddContext(CrfDesignContext db)
        {
            DB = db;
        }
        private CategoryRepository()
        {
        }

        public CrfOptionCategory GetById(int id)
        {
            if (DB == null)
                return null;
            return DB.CrfOptionCategories.Find(id);
        }

        public IEnumerable<CrfOptionCategory> GetAll()
        {
            if (DB == null)
                return null;
            return DB.CrfOptionCategories.ToList();
        }

    }
}
