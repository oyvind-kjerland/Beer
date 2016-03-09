using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    class BinaryToBinaryDeveloper : PhenotypeDeveloper
    {
        public override Phenotype Develop(Genotype genotype)
        {
            BinaryGenotype binaryGenotype = (BinaryGenotype)genotype;
            BinaryPhenotype binaryPhenotype = new BinaryPhenotype();
            binaryPhenotype.BitVector = (int[])binaryGenotype.BitVector.Clone();
            binaryPhenotype.NumBits = binaryGenotype.NumBits;

            return binaryPhenotype;

        }
    }
}
