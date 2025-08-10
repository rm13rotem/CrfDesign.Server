using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models.AdminManagement
{
    public class RuntimeEnvironment : IRuntimeEnvironment
    {
        private string _current = "Production"; // default
        public string Current => _current;

        public void Set(string environment)
        {
            if (environment == "Development" || environment == "Production")
                _current = environment;
        }
    }

}
