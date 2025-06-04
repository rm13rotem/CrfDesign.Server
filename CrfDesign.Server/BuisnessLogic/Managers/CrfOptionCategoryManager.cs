using BuisnessLogic.DataContext;
using BuisnessLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.Managers
{
    public class CrfOptionCategoryManager
    {
        private readonly CrfDesignContext _context;

        public CrfOptionCategoryManager(CrfDesignContext context)
        {
            _context = context;
        }

        public async Task<CrfOptionCategory> GetByIdAsync(int? id)
        {

            if (id == null)
            {
                return null;
            }

            var crfOptionCategory = await _context.CrfOptionCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            return crfOptionCategory; // null or not
        }
    }
}
