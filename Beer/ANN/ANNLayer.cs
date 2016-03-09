using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer
{
    class ANNLayer
    {

        // 2D array of weights where each row are the output weights of one node to all the other nodes of the next layer
        public double[,] Weights { get; set; }

        // activation function as sigmoid, tangent, etc
        public ActivationFunction ActivationFunction { get; set; }

        // The output of the layer
        public double[] output;

        public int numNodes { get; private set; }
        public int numOutputs { get; private set; }

        // Bias node settings
        private bool useBiasNode;

        public ANNLayer(int numNodes, int numOutputs, bool useBiasNode)
        {

            // Add an extra node if using bias node
            if (useBiasNode) numNodes++;
            this.useBiasNode = useBiasNode;

            this.numNodes = numNodes;
            this.numOutputs = numOutputs;

            
            Weights = new double[numNodes, numOutputs];

            output = new double[numOutputs];
        }

        public void Calculate(double[] input)
        {
            // Clear the output
            for (int i = 0; i < numOutputs; i++) output[i] = 0;

            double activation;

            // Iterate over each node in the layer
            for (int i=0; i<numNodes; i++)
            {

                // Get the activation
                if (useBiasNode && i==numNodes-1)
                {
                    activation = 1;
                } else
                {
                    activation = ActivationFunction.Activate(input[i]);
                }

                // Add the contribution to the outputs
                for (int j=0; j<numOutputs; j++)
                {
                    output[j] += Weights[i, j] * activation;

                }
            }   
        }
    }
}
