using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    class LOLZEvaluator : FitnessEvaluator
    {

        // Leading zeroes threshold
        public int Z { get; set; }

        public override float Evaluate(Individual individual)
        {
            BinaryPhenotype phenotype = (BinaryPhenotype)individual.Phenotype;

            int sumOnes = 0;

            // Look for leading ones
            for (int i=0; i<phenotype.NumBits; i++)
            {
                if (phenotype.BitVector[i] == 0) break;
                sumOnes++;
            }

            int sumZeros = 0;
            // Sum up leading zeroes up to threshold Z
            for (int i=0; i<phenotype.NumBits && i < Z; i++)
            {
                if (phenotype.BitVector[phenotype.NumBits - 1 - i] == 1) break;
                sumZeros++;
            }

            if (sumOnes > sumZeros)
            {
                return sumOnes;
            } else
            {
                return sumZeros;
            }
        }
    }
}
