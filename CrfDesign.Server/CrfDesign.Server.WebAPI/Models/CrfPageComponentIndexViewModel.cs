using CrfDesign.Server.WebAPI.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models
{
    public class CrfPageComponentIndexViewModel
    {
        public CrfPageComponentFilter Filter { get; set; }
        public IEnumerable<CrfPageComponentViewModel> Items { get; set; }

    }
}
