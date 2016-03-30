using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer
{
    class Neuron
    {
        public double upstreamInput { get; set; }

        public double internalState { get; set; }
        public double timeConstant { get; set; } = 1;
        public double gain { get; set; } = 4;
        public double output { get; set; }
        public double[] weights { get; set; }


        public Neuron (int numIn)
        {
            weights = new double[numIn];

            // test
            for (int i = 0; i < numIn; i++)
                weights[i] = 0.1;
        }

        public virtual void UpdateState()
        {
            // Calculate update value
            double dy = (1.0 / timeConstant) * (-internalState + upstreamInput);

            // Update internal state
            internalState += dy;
        }

        public virtual void UpdateOutput()
        {
            output = 1.0 / (1 + Math.Pow(Math.E, -gain * internalState));
        }

        public void Reset()
        {
            internalState = 0;
            output = 0;
        }
    }
}
