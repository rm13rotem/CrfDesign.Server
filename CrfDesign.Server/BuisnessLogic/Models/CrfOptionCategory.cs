﻿using BuisnessLogic.Interfaces;
using System;

namespace BuisnessLogic.Models
{
    public class CrfOptionCategory : IPersistantEntity
    {
        public int Id { get; set; }
        public string Name { get ; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}