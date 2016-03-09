using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA
{
    abstract class GeneticOperator
    {
        public float MutationRate { get; set; }
        public float CrossoverRate { get; set; }

        protected Random random;

        public GeneticOperator()
        {
            random = new Random();
        }

        abstract public void Mutate(Genotype genotype);
        abstract public void Crossover(Genotype parent1Genotype, Genotype parent2Genotype, Genotype child1Genotype, Genotype child2Genotype);
    }
}
