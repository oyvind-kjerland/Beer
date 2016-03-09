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

        // Number of sensor nodes
        private int numInputNodes;

        // Number of motor nodes
        private int numOutputNodes;

        // Hidden layers
        private int numLayers;
        private int[] numNodesPerLayer;


        
        public ANN(int numInputNodes, int numOutputNodes, int numLayers, int[] numNodesPerLayer, ActivationFunction activationFunction, bool useBiasNode)
        {

            this.numInputNodes = numInputNodes;
            this.numOutputNodes = numOutputNodes;
            this.numLayers = numLayers;
            this.numNodesPerLayer = numNodesPerLayer;

            // Create the list containing all layers, including sensor, hidden and motor
            layerList = new List<ANNLayer>();

            // Add the Sensor layer
            ANNLayer sensorLayer;
            if (numLayers > 0)
            {
                sensorLayer = new ANNLayer(numInputNodes, numNodesPerLayer[0], useBiasNode);
            } else
            {
                sensorLayer = new ANNLayer(numInputNodes, numOutputNodes, useBiasNode);
            }

            sensorLayer.ActivationFunction = new IdentityActivation();
            layerList.Add(sensorLayer);

            // Add the hidden layers
            if (numLayers > 0)
            {
                for (int i=0; i<numLayers-1; i++)
                {
                    ANNLayer hiddenLayer = new ANNLayer(numNodesPerLayer[i], numNodesPerLayer[i+1], useBiasNode);
                    hiddenLayer.ActivationFunction = activationFunction;
                    layerList.Add(hiddenLayer);
                }

                // Add the last hidden layer, which will give the outputs to the motor layer
                ANNLayer lastLayer = new ANNLayer(numNodesPerLayer[numLayers-1], numOutputNodes, useBiasNode);
                lastLayer.ActivationFunction = activationFunction;
                layerList.Add(lastLayer);
            }
        }


        public double[] Run(double[] input)
        {
            foreach (ANNLayer layer in layerList)
            {
                // Calculate the outputs of the layer
                layer.Calculate(input);

                // Set the input of the next layer to be the output of the calculated layer
                input = layer.output;
            }

            // Return the output of the last layer
            return layerList.Last().output;
        }


        public int GetNumberOfWeights()
        {
            int numWeights = 0;
            
            // Sum up all the weights of the layers
            foreach (ANNLayer layer in layerList)
            {
                numWeights += layer.numNodes * layer.numOutputs;
            }

            return numWeights;
        }


        public void SetWeights(double[] weights)
        {
            int w = 0;

            foreach (ANNLayer layer in layerList)
            {
                for (int i=0; i<layer.Weights.GetLength(0); i++)
                {
                    for (int j=0; j<layer.Weights.GetLength(1); j++)
                    {
                        layer.Weights[i, j] = weights[w];
                        w++;
                    }
                }
            }
        }

    }
}
