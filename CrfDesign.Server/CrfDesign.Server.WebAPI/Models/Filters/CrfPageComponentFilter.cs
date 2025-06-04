using BuisnessLogic.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models.Filters
{
    public class CrfPageComponentFilter : Filter
    {
        // string PartialName, int Page (=1), int NLines (=10)
        public int CrfPageId { get; set; }
    }
}
