using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer
{

    public enum Move : int { LEFT = -1, PULL = 0, RIGHT = 1 }

    public class BeerTracker : BeerObject
    {

        public ANN ann;
        public double[] Sensors;

        public Move currentMove;
        public int currentSpeed;
        private int maxSpeed = 4;

        public BeerTracker(int width) : base(width)
        {
            this.IsTracker = true;
            Sensors = new double[width];
        }

        public void GetMove(double[] senseUp)
        {
            
            List<double> outputs = ann.Run(senseUp);

            // Find the largest output
            int maxIndex = 0;
            double maxOutput = outputs[0];
            for (int i=1; i<outputs.Count; i++)
            {
                if (outputs[i] > maxOutput)
                {
                    maxOutput = outputs[i];
                    maxIndex = i;
                }
            }
            
            // Pull is included
            if (outputs.Count == 3)
            {
                currentMove = (Move)(maxIndex-1);

            // Only left and right movement
            } else
            {
                currentMove = (maxIndex == 0) ? Move.LEFT : Move.RIGHT;
            }

            // Calculate speed
            //double normalizedOutput = (maxOutput +  5) / 10.0;
            double normalizedOutput = maxOutput;

            currentSpeed = (int)Math.Round(maxSpeed * normalizedOutput);
            
        }
    }
}
