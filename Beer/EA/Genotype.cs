using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer
{
    abstract class Genotype
    {
        abstract public Genotype Clone();
        abstract public void Randomize(Random random);
        virtual public string GetGenotypeString() { return null; }
    }
}
