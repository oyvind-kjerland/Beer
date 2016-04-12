using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    class BinaryGenotype : Genotype
    {
        public int NumBits { get; set; }
        public int[] BitVector { get; set; }

        // Constructor
        public BinaryGenotype(int numBits)
        {
            this.NumBits = numBits;

            // Initialize bit vector
            BitVector = new int[numBits];
        }

        public override void Randomize(Random random)
        {
            for (int i=0; i<NumBits; i++)
            {
                BitVector[i] = random.Next(2);
            }
        }
        
        override public Genotype Clone()
        {
            BinaryGenotype newGenotype = new BinaryGenotype(NumBits);
            BitVector.CopyTo(newGenotype.BitVector, 0);
            return newGenotype;
        }
        
        override public string GetGenotypeString()
        {
            String bitstr = "";
            for (int i=0; i<BitVector.Length; i++)
            {
                bitstr += BitVector[i];
            }
            return bitstr;
        }
    }
}
