using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    class BinaryGeneticOperator : GeneticOperator
    {

        public override void Crossover(Genotype parent1Genotype, Genotype parent2Genotype, Genotype child1Genotype, Genotype child2Genotype)
        {
            float selectedValue = (float)random.NextDouble();

            if (selectedValue < CrossoverRate)
            {
                // Crossover

               
                // Choose random crossover point
                BinaryGenotype binaryParent1 = (BinaryGenotype)parent1Genotype;
                BinaryGenotype binaryParent2 = (BinaryGenotype)parent2Genotype;
                BinaryGenotype binaryChild1 = (BinaryGenotype)child1Genotype;
                BinaryGenotype binaryChild2 = (BinaryGenotype)child2Genotype;

                int numBits = binaryParent1.NumBits;
                int crossoverPoint = random.Next(numBits);

                // Child 1 gets 
                for (int i=0; i<numBits; i++)
                {
                    if (i < crossoverPoint)
                    {
                        binaryChild2.BitVector[i] = binaryParent1.BitVector[i];
                    } else
                    {
                        binaryChild1.BitVector[i] = binaryParent2.BitVector[i];
                    }
                }
            }
            return;
        }

        public override void Mutate(Genotype genotype)
        {
            BinaryGenotype binaryGenotype = (BinaryGenotype)genotype;
            for (int i = 0; i < binaryGenotype.NumBits; i++)
            {
                float selectedValue = (float)random.NextDouble();
                if (selectedValue < MutationRate)
                {
                    binaryGenotype.BitVector[i] ^= 1;
                }
            }
            return;
        }
    }
}
