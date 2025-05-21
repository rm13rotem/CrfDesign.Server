using BuisnessLogic.DataContext;
using BuisnessLogic.Interfaces;
using BuisnessLogic.Models;
using BuisnessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models
{
    public class CrfOptionViewModel : IPersistantEntity
    {
        public CrfOptionViewModel(CrfOption model, CrfDesignContext _context)
        {
           
            Id = model.Id;
            Name = model.Name;
            IsDeleted = model.IsDeleted;
            ModifiedDateTime = model.ModifiedDateTime;
            CrfOptionCategoryName = _context.CrfOptionCategories.Find(model.CrfOptionCategoryId)?.Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string CrfOptionCategoryName { get; set; }  // Foreign key to CRFQuestion
    }
}
