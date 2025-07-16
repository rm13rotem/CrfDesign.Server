using BuisnessLogic.Interfaces;
using System;

namespace BuisnessLogic.Models
{
    public class CrfOptionCategory : IPersistantEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLockedForChanges { get; set; }
        public string? LastUpdatorUserId { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public IPersistantEntity ToNewEntity()
        {
            CrfOptionCategory result = new()
            {
                Name = this.Name,
                IsDeleted = this.IsDeleted,
                ModifiedDateTime = this.ModifiedDateTime,
                IsLockedForChanges = this.IsLockedForChanges,
                LastUpdatorUserId = this.LastUpdatorUserId,
            };
            return result;
        }
    }
}