using BuisnessLogic.Filters;
using BuisnessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuisnessLogic.Interfaces.Managers
{
    public interface ICrfOptionsManager
    {
        Task<List<CrfOption>> GetCrfOptionsAsync(CrfOptionFilter crfOptionFilter);
    }
}
