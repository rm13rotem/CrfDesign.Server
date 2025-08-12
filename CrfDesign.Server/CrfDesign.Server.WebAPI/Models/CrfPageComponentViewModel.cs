using BuisnessLogic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrfDesign.Server.WebAPI.Models
{
    public class CrfPageComponentViewModel
    {
        public CrfPageComponentViewModel(
            CrfPageComponent component,
            string crfPageName,
            string questionType,
            string categoryName,
            IEnumerable<string> categoryOptions
        )
        {
            // Bulk copy matching properties from model → viewmodel
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(component), this);

            // Override / set extra resolved values
            CRFPageName = crfPageName;
            QuestionType = questionType;
            CategoryName = categoryName;
            CategoryOptionsList = categoryOptions?.ToList() ?? new List<string>();
        }

        public int Id { get; set; }
        public string CRFPageName { get; set; }
        public string QuestionText { get; set; }
        public string RenderType { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionType { get; set; }
        public bool IsRequired { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ValidationPattern { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public bool IsLockedForChanges { get; set; }
        public string LastUpdatorUserId { get; set; }

        [JsonIgnore]
        public List<string> CategoryOptionsList { get; set; }

        public string CategoryOptions =>
            CategoryOptionsList != null && CategoryOptionsList.Any()
                ? string.Join("\\", CategoryOptionsList)
                : (CategoryId > 0 ? CategoryName + "**" : string.Empty);
    }
}
