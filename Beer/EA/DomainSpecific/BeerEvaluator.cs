using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer.EA.DomainSpecific
{
    class BeerEvaluator : FitnessEvaluator
    {

        // Hardcoded level size
        private const int WORLD_WIDTH = 30;
        private const int WORLD_HEIGHT = 15;


        private BeerWorld beerWorld;


        public BeerEvaluator()
        {
            beerWorld = new BeerWorld(WORLD_WIDTH, WORLD_HEIGHT);

        }

        public override float Evaluate(Individual individual)
        {

            return 0.0;
            
        }
    }
}
