using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    abstract class GlobalParentSelector : ParentSelector
    {
        abstract public void Scale(List<Individual> adultPopulation);

        override public void Select(List<Individual> parentList, List<Individual> adultPopulation)
        {

            // Scale the fitness of the population
            Scale(adultPopulation);

            // Setup roulette wheel
            float currentFitness = 0;
            List<float> intervals = new List<float>(adultPopulation.Count);

            foreach (Individual individual in adultPopulation)
            {
                currentFitness += individual.ScaledFitness;
                intervals.Add(currentFitness);
            }

            // Clear parents list
            parentList.Clear();

            for (int i = 0; i < ChildCount; i++)
            {
                parentList.Add(SpinWheel(intervals, adultPopulation));
            }

            return;

        }

        protected Individual SpinWheel(List<float> intervals, List<Individual> adultPopulation)
        {

            // Get a random value between 0 and 1
            double selectedValue = random.NextDouble();
            // Special case for the first element
            if (selectedValue <= intervals[0])
            {
                return adultPopulation[0];
            }
            // Check all the other intervals
            for (int i = 0; i < intervals.Count - 1; i++)
            {
                if (selectedValue >= intervals[i] && selectedValue <= intervals[i + 1])
                {
                    return adultPopulation[i + 1];
                }

            }

            // It might happen that all the fitnesses add up to 0.9999 or something
            // return the last interval instead
            return adultPopulation[adultPopulation.Count - 1];
        }

        protected void NormalizeScaledFitness(List<Individual> adultPopulation)
        {
            float sum = 0;

            foreach (Individual individual in adultPopulation)
            {
                sum += individual.ScaledFitness;
            }

            foreach (Individual individual in adultPopulation)
            {
                individual.ScaledFitness /= sum;
            }
        }
    }
}
