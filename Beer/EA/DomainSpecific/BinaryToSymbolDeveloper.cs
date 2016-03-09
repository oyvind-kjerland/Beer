using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    class BinaryToSymbolDeveloper : PhenotypeDeveloper
    {
        public int SymbolSize { get; set; }
        public int S;

        public override Phenotype Develop(Genotype genotype)
        {
            SymbolPhenotype symbolPhenotype = new SymbolPhenotype();
            BinaryGenotype binaryGenotype = (BinaryGenotype)genotype;

            

            int symbol = 0;
            for (int i=0; i<binaryGenotype.NumBits; i++)
            {
                
                symbol += binaryGenotype.BitVector[i] << (i % SymbolSize);
                if (i % SymbolSize == SymbolSize-1)
                {

                    symbolPhenotype.symbols.Add(symbol % S);
                    symbol = 0;
                }
            }

            return symbolPhenotype;
        }
    }
}
