using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer
{
    class SigmoidActivation : ActivationFunction
    {
        public override double Activate(double input)
        {
            return 1.0f / (1 + Math.Pow(Math.E, -input));
        }
    }
}
