using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    class CTRNNPhenotype : Phenotype
    {

        public double[] Weights { get; set; }
        public double[] Gains { get; set; }
        public double[] TimeConstant { get; set; }


        public override string GetPhenotypeString()
        {
            string s = "";
            s += string.Join(",", Weights);
            s += "," + string.Join(",", Gains);
            s += "," + string.Join(",", TimeConstant);

            return s;
        }
    }
}
