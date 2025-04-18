﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models
{
    public class CrfOption : IPersistantEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int CrfQuestionId { get; set; }  // Foreign key to CRFQuestion
    }
}
