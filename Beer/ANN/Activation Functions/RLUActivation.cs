using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer
{
    class RLUActivation : ActivationFunction
    {
        public override double Activate(double input)
        {
            return (input >= 0.0) ? input : 0;
        }
    }
}
