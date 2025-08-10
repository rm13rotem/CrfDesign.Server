using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models.AdminManagement
{
    public interface IRuntimeEnvironment
    {
        string Current { get; }
        void Set(string environment);
    }

 
}
