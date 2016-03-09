using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA
{
    abstract class PhenotypeDeveloper
    {
        abstract public Phenotype Develop(Genotype genotype);
    }
}
