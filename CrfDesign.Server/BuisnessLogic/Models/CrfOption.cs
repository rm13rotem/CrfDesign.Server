using BuisnessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessLogic.Models
{
    public class CrfOption : IPersistantEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int CrfOptionCategoryId { get; set; }
        public int CrfQuestionId { get; set; }  // Foreign key to CRFQuestion
        public CrfOptionCategory OptionCategory { get; set; }
    }
}
