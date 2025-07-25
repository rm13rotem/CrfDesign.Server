using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Controllers
{
    [AllowAnonymous]
    public class HealthController : Controller
    {
        public IActionResult Index()
        {
            return Ok(true);
        }
    }
}
