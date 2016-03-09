using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    class SurprisingEvaluator : FitnessEvaluator
    {

        public bool Local;

        public override float Evaluate(Individual individual)
        {
            SymbolPhenotype phenotype = (SymbolPhenotype)individual.Phenotype;

            // Create a hashmap of the sequences
            HashSet<Int64> hashSet = new HashSet<Int64>();


            // Check how many local rules are broken
            int errors = 0;
            Int64 pair;

            int count;
            if (Local)
            {
                count = 1;
            } else
            {
                count = phenotype.symbols.Count-1;
            }

            for (int d=0; d<count; d++)
            {
                hashSet.Clear();
                for (int i=0; i<phenotype.symbols.Count-1-d; i++)
                {
                    // And two 32 bit integers into a 64 bit integer
                    pair = phenotype.symbols[i];
                    pair = pair << 32;
                    pair |= (long)phenotype.symbols[i + 1 + d];

                    if (hashSet.Contains(pair))
                    {
                        errors++;
                    } else
                    {
                        hashSet.Add(pair);
                    }
                }

            }
            // Return the max fitness minus the errors
            if (Local)
            {
                return phenotype.symbols.Count - errors;
            } else
            {
                return (phenotype.symbols.Count - 2) * (phenotype.symbols.Count - 3) / 2 - errors;
            }
            //return 1.0f / (1 + errors);
        }
    }
}
