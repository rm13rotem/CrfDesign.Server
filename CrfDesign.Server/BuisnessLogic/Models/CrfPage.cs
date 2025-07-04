﻿using BuisnessLogic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessLogic.Models
{
    public class CrfPage : IPersistantEntity
    {
        public int Id { get; set; }
        public int StudyId { get; set; }  // Foreign key to Study
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsLockedForChanges { get; set; }

        // Navigation property to Questions
        [JsonIgnoreAttribute]
        public ICollection<CrfPageComponent> Questions { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
