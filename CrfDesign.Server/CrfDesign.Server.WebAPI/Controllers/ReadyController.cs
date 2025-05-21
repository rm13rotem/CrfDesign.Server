using BuisnessLogic.DataContext;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Controllers
{
    
    public class ReadyController : Controller
    {
        private readonly CrfDesignContext _context;

        public ReadyController(CrfDesignContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var crfOption = _context.CrfOptions.FirstOrDefault();
            var crfOptionCategory = _context.CrfOptionCategories.FirstOrDefault();
            var crfPage = _context.CrfPages.FirstOrDefault();
            var crfPageComponent = _context.CrfPageComponents.FirstOrDefault();

            var errors = new StringBuilder();
            if (crfOption == null)
                errors.Append("crfOption table empty"); 
            if (crfOptionCategory == null)
                errors.Append("crfOptionCategory table empty"); 
            if (crfPage == null)
                errors.Append("crfPage table empty");
            if (crfPageComponent == null)
                errors.Append("crfPageComponent table empty");

            if (errors.ToString().Length == 0)
                return Ok(true);
            return Problem(errors.ToString());
        }
    }
}
