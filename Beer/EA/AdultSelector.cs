using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA
{
    abstract class AdultSelector
    {

        public int AdultCount { get; set; }
        abstract public void Select(List<Individual> childPopulation, List<Individual> adultPopulation);
    }
}
