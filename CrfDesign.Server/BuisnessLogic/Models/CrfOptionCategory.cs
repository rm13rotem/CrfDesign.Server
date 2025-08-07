using BuisnessLogic.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace BuisnessLogic.Models
{
    public class CrfOptionCategory : IPersistantEntity
    {
        [JsonIgnore]
        public int Id { get; set; }

        
        public string Name { get; set; }

        [DisplayName("Is Deleted")]
        public bool IsDeleted { get; set; }

        [DisplayName("Is Locked for Changes")]
        public bool IsLockedForChanges { get; set; }

        [DisplayName("Last Updator (User Id)")]
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