using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    class SigmaScaling : GlobalParentSelector
    {
        public override void Scale(List<Individual> adultPopulation)
        {
            float average = 0;
            float sd = 0;

            // Find average
            foreach (Individual individual in adultPopulation)
            {
                average += individual.Fitness;
            }

            average /= adultPopulation.Count;

            // Find standard variation
            foreach (Individual individual in adultPopulation)
            {
                sd += (float)Math.Pow(individual.Fitness - average, 2);
            }
            sd /= adultPopulation.Count;
            sd = (float)Math.Sqrt(sd);

            // Scale fitness values
            foreach (Individual individual in adultPopulation)
            {
                individual.ScaledFitness = 1 + (individual.Fitness - average) / (2 * sd);
            }

            NormalizeScaledFitness(adultPopulation);
            return;
        }
    }
}
