using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer
{
    class Bias : Neuron
    {

        public Bias() : base(0)
        {
        }

        public override void UpdateState()
        {
            return;
        }

        public override void UpdateOutput()
        {
            base.output = 1.0;
        }

    }
}
