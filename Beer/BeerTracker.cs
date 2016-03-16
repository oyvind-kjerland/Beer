using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer
{

    public enum Move : int { LEFT = -1, NONE = 0, RIGHT = 1 }

    public class BeerTracker : BeerObject
    {

        public ANN ann;
        public double[] Sensors;

        public BeerTracker(int width) : base(width)
        {
            this.IsTracker = true;
        }

        public Move GetMove(double[] senseUp)
        {
            Sensors = senseUp;
            List<double> outputs = ann.Run(senseUp);

            // Find largest output
            double max = double.MinValue;
            int maxi = 0;
            for (int i=0; i<outputs.Count; i++)
            {
                if (outputs[i] > max)
                {
                    max = outputs[i];
                    maxi = i;
                }
            }
            maxi -= 1;

            return (Move)maxi;
        }


    }
}
