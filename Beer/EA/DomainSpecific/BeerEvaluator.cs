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


        // Settings
        public int TimeSteps { get; set; }

        public float CatchWeight = 1;
        public float MissWeight = -1;
        public float AvoidWeight = 1;
        public float HitWeight = -1;

        public BeerEvaluator()
        {
            beerWorld = new BeerWorld(WORLD_WIDTH, WORLD_HEIGHT);
        }

        public override float Evaluate(Individual individual)
        {

            CTRNNPhenotype phenotype = (CTRNNPhenotype)individual.Phenotype;
            beerWorld.Tracker.ann.Setup(phenotype.Weights, phenotype.Gains, phenotype.TimeConstant);


            // Reset
            beerWorld.ResetWorld();

            // Simulate
            for (int i=0; i<TimeSteps; i++)
            {
                beerWorld.Update();
            }

            // Gather results and aggregate
            float fitness = beerWorld.ObjectsCaught * CatchWeight + beerWorld.ObjectsMissed * MissWeight
                + beerWorld.ObjectsAvoided * AvoidWeight + beerWorld.ObjectsHit * HitWeight;
            
            return fitness;
            
        }
    }
}
