using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Beer.EA
{
    class FullSelector : AdultSelector
    {
        public override void Select(List<Individual> childPopulation, List<Individual> adultPopulation)
        {
            // Clear the adult population
            adultPopulation.Clear();

            // Fill with children
            adultPopulation.AddRange(childPopulation);
            //foreach (Individual individual in childPopulation)
            //{
            //    adultPopulation.Add(individual);
            //}
            adultPopulation.Sort();
            adultPopulation.Reverse();

            // Test
            //adultPopulation.Reverse();

            // Clear child population
            childPopulation.Clear();
        }
    }
}
