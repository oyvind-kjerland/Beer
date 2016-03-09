using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    class ANNWeightPhenotype : Phenotype
    {

        public double[] Weights { get; set; }

        public override string GetPhenotypeString()
        {
            return string.Join(",",Weights);
        }
    }
}
