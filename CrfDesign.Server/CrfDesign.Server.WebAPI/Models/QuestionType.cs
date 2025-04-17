using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models
{
    public class QuestionType : IPersistantEntity
    {
        public int Id { get; set; }
        public string Name { get ; set; }
        public bool IsDeleted { get ; set; }
        public DateTime ModifiedDateTime { get; set; }
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
