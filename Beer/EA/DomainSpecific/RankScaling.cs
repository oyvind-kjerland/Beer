using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    class RankScaling : GlobalParentSelector
    {

        public float Max { get; set; }
        public float Min { get; set; }

        public override void Scale(List<Individual> adultPopulation)
        {
            // Sort the adult population
            adultPopulation.Sort();

            // Perform rank scaling
            for (int i=0; i<adultPopulation.Count; i++)
            {
                adultPopulation[i].ScaledFitness = Min + (Max - Min) * (adultPopulation.Count - i) / (adultPopulation.Count - 1);
            }

            // Normalize
            NormalizeScaledFitness(adultPopulation);
        }
    }
}
