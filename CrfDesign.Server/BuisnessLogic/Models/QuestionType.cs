using BuisnessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessLogic.Models
{
    public class QuestionType : IPersistantEntity
    {
        public int Id { get; set; }
        public string Name { get ; set; }
        public bool IsDeleted { get ; set; }
        public DateTime ModifiedDateTime { get; set; }
        public IPersistantEntity ToNewEntity()
        {
            QuestionType result = new()
            {
                Name = this.Name,
                IsDeleted = this.IsDeleted,
                ModifiedDateTime = this.ModifiedDateTime
            };
            return result;
        }
    }
    //    Text,
    //    MultipleChoice,
    //    SingleChoice,
    //    Date,
    //    Checkbox,
    //    Numeric,
    //    Boolean, 
    //    OpenSingleChoise
    //}
}
