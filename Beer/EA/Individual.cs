using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer
{
    class Individual : IComparable
    {
        // The individual's genotype
        public Genotype Genotype { get; set; }

        // The individual's phenotype
        public Phenotype Phenotype { get; set; }

        // The individual's fitness
        public float Fitness { get; set; }

        // The individual's scaled fitness
        public float ScaledFitness { get; set; }

        // Comparison
        public int CompareTo(object obj)
        {
            Individual other = obj as Individual;

            return -Fitness.CompareTo(other.Fitness);
        }
    }
}
