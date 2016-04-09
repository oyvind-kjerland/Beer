using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    class BinaryToCTRNNWeightsDeveloper : PhenotypeDeveloper
    {

        public int NumBitsPerUnit { get; set; }
        public int NumWeights { get; set; }
        public int NumGains { get; set; }
        public int NumTimeConstants { get; set; }
        public int[] BiasIndices { get; set; }


        public override Phenotype Develop(Genotype genotype)
        {
            BinaryGenotype binaryGenotype = (BinaryGenotype)genotype;

            CTRNNPhenotype ctrnntPhenotype = new CTRNNPhenotype();

            double max = Math.Pow(2, NumBitsPerUnit);

            int totalWeightsBitLength = NumWeights * NumBitsPerUnit;
            int totalGainsBitLength = NumGains * NumBitsPerUnit;
            int totalTimeConstantsBitLength = NumTimeConstants * NumBitsPerUnit;

            double[] allWeights = new double[NumWeights];
            double weight = 0;
            int weightIndex = 0;


            // Generate weight part
            for (int i = 0; i < totalWeightsBitLength; i++)
            {

                // Convert bits to an int
                weight += binaryGenotype.BitVector[i] << (i % NumBitsPerUnit);

                if (i % NumBitsPerUnit == NumBitsPerUnit - 1)
                {
                    // Calculating a weight between -5 and 5
                    weight = (((double)(weight) / max) * 10) - 5;

                    allWeights[weightIndex] = weight;
                    weight = 0;
                    weightIndex++;
                }
            }

            // Fix bias weights
            
            int biasIndex = 0;
            double[] biasWeights = new double[BiasIndices.Length];
            double[] weights = new double[NumWeights - BiasIndices.Length];
            weightIndex = 0;

            for (int i=0; i<allWeights.Length; i++)
            {
                if (biasIndex < BiasIndices.Length && i == BiasIndices[biasIndex])
                {
                    biasWeights[biasIndex++] = allWeights[i] - 5;
                } else
                {
                    weights[weightIndex++] = allWeights[i];
                }
            }




            double[] gains = new double[NumGains];
            int gain = 0;
            int gainIndex = 0;
            // Generate gains part
            for (int i = totalWeightsBitLength; i < totalGainsBitLength + totalWeightsBitLength; i++)
            {

                // Convert bits to an int
                gain += binaryGenotype.BitVector[i] << (i % NumBitsPerUnit);

                if (i % NumBitsPerUnit == NumBitsPerUnit - 1)
                {
                    // Calculating a weight between 1 and 5
                    gains[gainIndex] = (((double)(gain) / max) * 4) + 1;
                    gain = 0;
                    gainIndex++;
                }
            }

            double[] timeConstants = new double[NumTimeConstants];
            int timeConstant = 0;
            int tcIndex = 0;
            // Generate weight part
            for (int i = totalGainsBitLength + totalWeightsBitLength; i < totalTimeConstantsBitLength + totalGainsBitLength + totalWeightsBitLength; i++)
            {

                // Convert bits to an int
                timeConstant += binaryGenotype.BitVector[i] << (i % NumBitsPerUnit);

                if (i % NumBitsPerUnit == NumBitsPerUnit - 1)
                {
                    // Calculating a weight between 1 and 2
                    timeConstants[tcIndex] = ((double)(timeConstant) / max) + 1;
                    timeConstant = 0;
                    tcIndex++;
                }
            }

            ctrnntPhenotype.Weights = weights;
            ctrnntPhenotype.Gains = gains;
            ctrnntPhenotype.TimeConstant = timeConstants;
            ctrnntPhenotype.BiasWeights = biasWeights;

            return ctrnntPhenotype;
        }

    }
}
