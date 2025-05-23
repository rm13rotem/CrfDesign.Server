using BuisnessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models
{
    public class RenderCrfPageViewModel
    {
        public RenderCrfPageViewModel(CrfPage crfPage, List<CrfPageComponent> crfPageComponent)
        {
            CrfPage = crfPage;
            CrfPageComponent = crfPageComponent;
        }

        public CrfPage CrfPage { get; }
        public List<CrfPageComponent> CrfPageComponent { get; }
    }
}
