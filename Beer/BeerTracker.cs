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
        private int maxSpeed = 3;

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


            // Get speed
            bool includeZero = false;

            if (includeZero)
            {
                if (maxOutput <= 0.2)
                {
                    currentSpeed = 4;
                } else if (maxOutput <= 0.4)
                {
                    currentSpeed = 3;
                } else if (maxOutput <= 0.6)
                {
                    currentSpeed = 2;
                } else if (maxOutput <= 0.8)
                {
                    currentSpeed = 1;
                } else if (maxOutput <= 1.0)
                {
                    currentSpeed = 0;
                }
            } else
            {
                if (maxOutput <= 0.25)
                {
                    currentSpeed = 4;
                } else if (maxOutput <= 0.5)
                {
                    currentSpeed = 3;
                } else if (maxOutput <= 0.75)
                {
                    currentSpeed = 2;
                } else if (maxOutput <= 1)
                {

                    currentSpeed = 1;
                }
            }
            

            
            
        }
    }
}
