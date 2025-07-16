using BuisnessLogic.DataContext;
using BuisnessLogic.Interfaces;
using BuisnessLogic.Models;
using System;

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
            LastUpdatorUserId = model.LastUpdatorUserId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLockedForChanges { get; set; }
        public string? LastUpdatorUserId { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string CrfOptionCategoryName { get; set; }  // Foreign key to CRFQuestion

        public IPersistantEntity ToNewEntity()
        {
            return this;
        }
    }
}
