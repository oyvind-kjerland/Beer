using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Beer.EA
{
    abstract class ParentSelector
    {
        protected Random random;

        public int ChildCount { get; set; }

        public ParentSelector()
        {
            random = new Random();
        }
        abstract public void Select(List<Individual> parentList, List<Individual> adultPopulation);
    }

}
