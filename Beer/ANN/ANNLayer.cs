using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer
{
    class ANNLayer
    {
        public Neuron[] nodes;

        // Bias node settings
        private bool useBiasNode;

        public ANNLayer(int numNodes, int numInputs, bool useBiasNode)
        {
            this.useBiasNode = useBiasNode;

            // Add an extra node if using bias node
            if (useBiasNode) numNodes++;

            nodes = new Neuron[numNodes];

            // First node is bias neuron, represent it as null

            if (useBiasNode) nodes[0] = new Bias();

            // Populate rest of array with CTR neurons
            int biasCount = (useBiasNode) ? 1 : 0;
            int numWeights = numNodes + numInputs;
            for (int i = biasCount; i < numNodes; i++)
            {
                nodes[i] = new Neuron(numWeights);
            }
            
        }

        public void Calculate(List<Neuron> prevNodes)
        {
            foreach (Neuron node in nodes)
            {
                if (!(node is Bias)) prevNodes.Add(node);
            }

            // Integrate upstream neighbour sum #eq.1 from previous layer and current layer
            for (int i = 0; i < prevNodes.Count; i++)
            {
                for (int j = 1; j < nodes.Length; j++)
                {
                    nodes[j].upstreamInput += prevNodes[i].output * nodes[j].weights[i];
                }
            }

            foreach (Neuron n in nodes)
            {
                n.UpdateState();
                n.UpdateOutput();
                n.upstreamInput = 0;
            }
        }

        public void SetOutput(double[] input)
        {
            for (int i = 0; i < input.Length; i++)
                nodes[i].output = input[i];
        }

        public List<double> GetOutput()
        {
            List<double> output = new List<double>();
            /*foreach (Neuron n in nodes)
            {
                output.Add(n.output);
            }*/

            for (int i=1; i<nodes.Length; i++)
            {
                output.Add(nodes[i].output);
            }

            return output;
        }

        public List<Neuron> GetNeurons()
        {
            return nodes.ToList<Neuron>();
        }
    }
}
