﻿using BuisnessLogic.Interfaces;
using BuisnessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models
{
    public class CrfPageViewModel : IPersistantEntity
    {
        public CrfPageViewModel(CrfPage crfPage)
        {
            this.Id = crfPage.Id;
            this.StudyId = crfPage.StudyId;
            this.Name = crfPage.Name;
            this.Description = crfPage.Description;
            this.CreatedAt = crfPage.CreatedAt;
                this.ModifiedDateTime = DateTime.Now;
            this.IsLockedForChanges = crfPage.IsLockedForChanges;
            this.LastUpdatorUserId = crfPage.LastUpdatorUserId;
            this.IsDeleted = crfPage.IsDeleted;
            this.LastUpdatorUserId = crfPage.LastUpdatorUserId;
        }
        public int Id { get; set; }
        public int StudyId { get; set; }  // Foreign key to Study
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsLockedForChanges { get; set; }
        public string? LastUpdatorUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public IPersistantEntity ToNewEntity()
        {
            return this;
        }
    }
}
