using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.Filters
{
    public abstract class Filter : IFilter
    {
        public string PartialName { get; set; }
        public int Page { get; set; } = 1;
        public int NLines { get; set; } = 10;
        public int TotalLines { get; set; }
        public int TotalPages { get {
                if (TotalLines > 0 && NLines > 0)
                    return (TotalLines / NLines) + 1;
                else return 1;
            } }
    }
}
