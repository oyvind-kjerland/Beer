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

            double[] weights = new double[NumWeights];
            int weight = 0;
            int weightIndex = 0;
            int biasWeightIndex = 0;

            // Generate weight part
            for (int i = 0; i < totalWeightsBitLength; i++)
            {

                // Convert bits to an int
                weight += binaryGenotype.BitVector[i] << (i % NumBitsPerUnit);

                if (i % NumBitsPerUnit == NumBitsPerUnit - 1)
                {
                    // Multiplier for scaling the weight interval for bias weights
                    int multiplier = (weightIndex == BiasIndices[biasWeightIndex]) ? -5 : 0;
                    // Calculating a weight between -10 and 0    
                    weights[weightIndex] = (((double)(weight - max) / max) * 5) + multiplier;
                    weight = 0;
                    weightIndex++;

                    if (weightIndex == BiasIndices[biasWeightIndex]) biasWeightIndex++;
                }
            }

            double[] gains = new double[NumGains];
            int gain = 0;
            int gainIndex = 0;
            // Generate gains part
            for (int i = totalWeightsBitLength; i < totalGainsBitLength; i++)
            {

                // Convert bits to an int
                gain += binaryGenotype.BitVector[i] << (i % NumBitsPerUnit);

                if (i % NumBitsPerUnit == NumBitsPerUnit - 1)
                {
                    // Calculating a weight between 1 and 5
                    gains[gainIndex] = (((double)(gain - max / 2) / max) * 2) + 3;
                    gain = 0;
                    gainIndex++;
                }
            }

            double[] timeConstants = new double[NumTimeConstants];
            int timeConstant = 0;
            int tcIndex = 0;
            // Generate weight part
            for (int i = totalGainsBitLength; i < totalTimeConstantsBitLength; i++)
            {

                // Convert bits to an int
                timeConstant += binaryGenotype.BitVector[i] << (i % NumBitsPerUnit);

                if (i % NumBitsPerUnit == NumBitsPerUnit - 1)
                {
                    // Calculating a weight between 1 and 2
                    timeConstants[tcIndex] = ((double)(timeConstant - max / 2) / max) + 2;
                    timeConstant = 0;
                    tcIndex++;
                }
            }

            ctrnntPhenotype.Weights = weights;
            ctrnntPhenotype.Gains = gains;
            ctrnntPhenotype.TimeConstant = timeConstants;

            return ctrnntPhenotype;
        }

    }
}
