using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer
{
    class HyperbolicTangentActivation : ActivationFunction
    {
        public override double Activate(double input)
        {
            return (2.0 / (1 + Math.Pow(Math.E, -2 * input))) - 1;
        }
    }
}
