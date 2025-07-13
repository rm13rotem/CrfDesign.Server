using BuisnessLogic.Interfaces;
using BuisnessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models
{
    public class QuestionTypeViewModel : IPersistantEntity
    {
        public QuestionTypeViewModel(QuestionType questionType)
        {

        }
        public int Id { get; set; }
        public string Name { get ; set; }
        public bool IsDeleted { get ; set; }
        public DateTime ModifiedDateTime { get; set; }

        public IPersistantEntity ToNewEntity()
        {
            return this;
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
