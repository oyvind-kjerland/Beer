using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Beer.EA.DomainSpecific
{
    class OneMaxEvaluator : FitnessEvaluator
    {

        public int[] Goal { get; set; }
        
        public override float Evaluate(Individual individual)
        {
            BinaryPhenotype binaryPhenotype = (BinaryPhenotype)individual.Phenotype;

            int numBits = binaryPhenotype.NumBits;

            int[] bitVector = binaryPhenotype.BitVector;

            // Sum number of ones
            int sum = 0;
            for (int i = 0; i < numBits; i++)
            {
                if (bitVector[i] == Goal[i]) sum++;
            }
            
            // Make a minimum fitness value
            if (sum == 0)
            {
                return 0.1f;
            } else
            {
                return (float)sum;
            }
        }
    }
}
