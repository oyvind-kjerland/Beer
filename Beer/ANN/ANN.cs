using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer
{
    public class ANN
    {
        private List<ANNLayer> layerList;
        
        public ANN(int numInputNodes, int numOutputNodes, int numLayers, int[] numNodesPerLayer, 
            ActivationFunction activationFunction, bool useBiasNode)
        {

            // Create the list containing all layers, including sensor, hidden and motor
            layerList = new List<ANNLayer>();

            // Add the Sensor layer, the sensor layer has no weights
            ANNLayer sensorLayer = new ANNLayer(numInputNodes, 0, false);
            layerList.Add(sensorLayer);

            // Add the hidden layers
            int previousNumInputs = numInputNodes;
            if (numLayers > 0)
            {
                for (int i=0; i<numLayers; i++)
                {
                    ANNLayer hiddenLayer = new ANNLayer(numNodesPerLayer[i], previousNumInputs, useBiasNode);
                    layerList.Add(hiddenLayer);
                    previousNumInputs = numNodesPerLayer[i];
                }
            }

            // Finally add motor layer
            ANNLayer motorLayer = new ANNLayer(numOutputNodes, previousNumInputs, useBiasNode);
            layerList.Add(motorLayer);

        }

        public void Setup(double[] weights, double[] biasWeights, double[] gains, double[] timeConstants)
        {
            int gainIndex = 0;
            int weightIndex = 0;
            int biasWeightIndex = 0;

            for (int layerIndex=1; layerIndex<layerList.Count; layerIndex++) 
            {

                ANNLayer layer = layerList[layerIndex];
                
                for (int nodeIndex=1; nodeIndex<layer.nodes.Length; nodeIndex++)
                {
                    
                    Neuron node = layer.nodes[nodeIndex];

                    node.gain = gains[gainIndex];
                    node.timeConstant = timeConstants[gainIndex];
                    gainIndex++;

                    node.weights[0] = biasWeights[biasWeightIndex];
                    biasWeightIndex++;
                    for (int w=1; w<node.weights.Length; w++)
                    {
                        node.weights[w] = weights[weightIndex];
                        weightIndex++;
                    }

                }
            }
        }

        public int GetNumberOfWeights()
        {
            int total = 0;
            
            for (int i=1; i<layerList.Count; i++)
            {
                ANNLayer layer = layerList[i];
                for (int n=1; n<layer.nodes.Length; n++)
                {
                    Neuron node = layer.nodes[n];

                    total += node.weights.Length;
                }
            }
            return total;
        }

        public int GetNumberOfGains()
        {
            int total = 0;
            for (int i=1; i<layerList.Count; i++)
            {
                total += layerList[i].nodes.Length - 1;
            }
            return total;
        }
        
        public List<double> Run(double[] input)
        {
            // Set motor input as the sensors layers output
            List<Neuron> prevNodes = layerList[0].GetNeurons();
            layerList[0].SetOutput(input);

            // Propagate the inputs through the network
            for (int i = 1; i < layerList.Count; i++)
            {
                layerList[i].Calculate(prevNodes);
                prevNodes = layerList[i].GetNeurons();
            }

            // Return output from motor layer
            return layerList.Last().GetOutput();
        }
    }
}
