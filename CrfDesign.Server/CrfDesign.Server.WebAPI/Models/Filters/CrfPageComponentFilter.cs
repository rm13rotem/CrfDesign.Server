using BuisnessLogic.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models.Filters
{
    public class CrfPageComponentFilter : IFilter
    {
        public string PartialName { get; set; }
        public int CrfPageId { get; set; }
    }
}
