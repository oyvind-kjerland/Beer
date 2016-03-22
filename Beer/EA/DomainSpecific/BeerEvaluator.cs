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


        public BeerWorld BeerWorld { get; set; }


        // Settings
        public int TimeSteps { get; set; }

        public float CatchWeight = 1;
        public float MissWeight = -1;
        public float AvoidWeight = 1.5f;
        public float HitWeight = -1;

        public BeerEvaluator()
        {
            BeerWorld = new BeerWorld(WORLD_WIDTH, WORLD_HEIGHT);
        }

        public override float Evaluate(Individual individual)
        {

            CTRNNPhenotype phenotype = (CTRNNPhenotype)individual.Phenotype;
            BeerWorld.Tracker.ann.Setup(phenotype.Weights, phenotype.BiasWeights, phenotype.Gains, phenotype.TimeConstant);


            // Reset
            BeerWorld.ResetWorld();

            // Simulate
            for (int i=0; i<TimeSteps; i++)
            {
                BeerWorld.Update();
            }

            // Gather results and aggregate
            float fitness = BeerWorld.ObjectsCaught * CatchWeight + BeerWorld.ObjectsMissed * MissWeight
                + BeerWorld.ObjectsAvoided * AvoidWeight + BeerWorld.ObjectsHit * HitWeight;
            
            return fitness;
            
        }
    }
}
