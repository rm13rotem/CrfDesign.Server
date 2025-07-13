using BuisnessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessLogic.Models
{
    public class CrfOption : IPersistantEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int CrfOptionCategoryId { get; set; }

        public IPersistantEntity ToNewEntity()
        {
            CrfOption result = new()
            {
                Name = this.Name,
                IsDeleted = this.IsDeleted,
                ModifiedDateTime = this.ModifiedDateTime,
                CrfOptionCategoryId = this.CrfOptionCategoryId
            };
            return result;
        }
    }
}
