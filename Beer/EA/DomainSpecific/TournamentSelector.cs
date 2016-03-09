using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    class TournamentSelector : ParentSelector
    {
        public int K { get; set; }
        public float E { get; set; }

        public override void Select(List<Individual> parentList, List<Individual> adultPopulation)
        {
            // Clear parent list
            parentList.Clear();

            List<Individual> group = new List<Individual>(K);
            Individual best;

            for (int n=0; n<ChildCount; n++)
            {
                group.Clear();

                // Select a group
                for (int i=0; i < K; i++)
                {
                    group.Add(adultPopulation[random.Next(adultPopulation.Count)]);
                }

                // Find the best individual
                best = group[0];
                for (int i=1; i < K; i++)
                {
                    if (group[i].Fitness > best.Fitness)
                    {
                        best = group[i];
                    }
                }

                // Select the best with a random probability of 1 - E, otherwise a random individual
                float selectedValue = (float)random.NextDouble();

                if (selectedValue < 1 - E)
                {
                    parentList.Add(best);
                } else
                {
                    parentList.Add(group[random.Next(K)]);
                }
            }
        }
    }
}
