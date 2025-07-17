using BuisnessLogic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessLogic.Models
{
    public class CrfPage : IPersistantEntity
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int StudyId { get; set; }  // Foreign key to Study
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsLockedForChanges { get; set; }
        public string? LastUpdatorUserId { get; set; }

        // Navigation property to Questions
        public ICollection<CrfPageComponent> Components { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public IPersistantEntity ToNewEntity()
        {
            CrfPage result = new()
            {
                StudyId = StudyId,
                CreatedAt = CreatedAt,
                Description = Description,
                IsLockedForChanges = IsLockedForChanges,
                Components = Components,
                Name = this.Name,
                IsDeleted = this.IsDeleted,
                ModifiedDateTime = this.ModifiedDateTime,
                LastUpdatorUserId = this.LastUpdatorUserId,
            };
            return result;
        }
    }
}
