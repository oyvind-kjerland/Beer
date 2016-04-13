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

        public float CatchWeight;
        public float MissWeight;
        public float AvoidWeight;
        public float HitWeight;

        public BeerEvaluator()
        {
            BeerWorld = new BeerWorld(WORLD_WIDTH, WORLD_HEIGHT);
        }

        public void SetWeights(int problem_index)
        {
            switch(problem_index)
            {
                // STANDARD
                case 0:     
                    CatchWeight = 1;
                    MissWeight = -1;
                    AvoidWeight = 1.25f;
                    HitWeight = -1f;
                    break;
                // PULL
                case 1:
                    CatchWeight = 1.5f;
                    MissWeight = -1f;
                    AvoidWeight = 1f;
                    HitWeight = -1.25f;
                    break;
                // NO WRAP
                case 2:
                    CatchWeight = 1.5f;
                    MissWeight = -1f;
                    AvoidWeight = 1.2f;
                    HitWeight = -1f;
                    break;
            }

        }

        public override void NextGeneration()
        {
            BeerWorld.NewSequence();
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
