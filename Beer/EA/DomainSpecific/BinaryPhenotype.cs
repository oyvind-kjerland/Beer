using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    class BinaryPhenotype : Phenotype
    {
        public int NumBits { get; set; }
        public int[] BitVector { get; set; }

        // Constructor
        public BinaryPhenotype()
        {
            
        }

        public override string GetPhenotypeString()
        {
            return string.Join(",",BitVector);
        }
    }
}
