using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    class BinaryToANNWeightsDeveloper : PhenotypeDeveloper
    {

        public int NumBitsPerWeight { get; set; }
        public int NumWeights { get; set; }

        public override Phenotype Develop(Genotype genotype)
        {
            BinaryGenotype binaryGenotype = (BinaryGenotype)genotype;

            ANNWeightPhenotype annWeightPhenotype = new ANNWeightPhenotype();

            double max = Math.Pow(2, NumBitsPerWeight);

            double[] weights = new double[NumWeights];
            int weight = 0;
            int weightIndex = 0;

            for (int i=0; i<binaryGenotype.BitVector.Length; i++)
            {

                // Convert bits to an int
                weight += binaryGenotype.BitVector[i] << (i % NumBitsPerWeight);

                if (i % NumBitsPerWeight == NumBitsPerWeight - 1)
                {
                    // Calculating a weight between 0 and 1
                    weights[weightIndex] = (double)(weight - max/2) / max;
                    weight = 0;
                    weightIndex++;
                }
            }

            annWeightPhenotype.Weights = weights;
            return annWeightPhenotype;
        }
    }
}
