using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beer
{
    public partial class NN_Visualizer : Form
    {

        ANN ann;
        int digits = 2;

        public NN_Visualizer(ANN ann)
        {
            InitializeComponent();
            this.ann = ann;
            UpdateLabels();
        }

        private string GetFormattedValue(double v)
        {
            return Math.Round(v, digits).ToString();
        }

        public void UpdateLabels()
        {
            List<ANNLayer> layers =  ann.GetLayers();

            //
            // Update weights going in to first hidden layer
            //
            ANNLayer layer1 = layers[1];

            // First node
            double[] weights = layer1.nodes[1].weights;
            label12.Text = GetFormattedValue(weights[0]);   // Bias
            label1.Text = GetFormattedValue(weights[1]);
            label3.Text = GetFormattedValue(weights[2]);
            label5.Text = GetFormattedValue(weights[3]);
            label7.Text = GetFormattedValue(weights[4]);
            label9.Text = GetFormattedValue(weights[5]);
            label17.Text = GetFormattedValue(weights[6]);   // Hidden neighbour
            label15.Text = GetFormattedValue(weights[7]);   // Itself
            label27.Text = "T1: " + layer1.nodes[1].timeConstant;
            label28.Text = "G1: " + layer1.nodes[1].gain;


            // Second node
            weights = layer1.nodes[2].weights;
            label11.Text = GetFormattedValue(weights[0]);   // Bias
            label2.Text = GetFormattedValue(weights[1]);
            label4.Text = GetFormattedValue(weights[2]);
            label6.Text = GetFormattedValue(weights[3]);
            label8.Text = GetFormattedValue(weights[4]);
            label10.Text = GetFormattedValue(weights[5]);
            label16.Text = GetFormattedValue(weights[6]);   // Hidden neighbour
            label18.Text = GetFormattedValue(weights[7]);   // Itself
            label29.Text = "T2: " + layer1.nodes[2].timeConstant;
            label30.Text = "G2: " + layer1.nodes[2].gain;

            //
            // Update weights going into motor layer
            //

            ANNLayer layer2 = layers[2];


            // First node
            weights = layer2.nodes[1].weights;
            label13.Text = GetFormattedValue(weights[0]);   //Bias
            label22.Text = GetFormattedValue(weights[1]);
            label21.Text = GetFormattedValue(weights[2]);
            label26.Text = GetFormattedValue(weights[3]);   // Motor neighbour
            label24.Text = GetFormattedValue(weights[4]);   // Itself
            label31.Text = "T1: " + layer2.nodes[1].timeConstant;
            label32.Text = "G1: " + layer2.nodes[1].gain;

            // Second node
            weights = layer2.nodes[2].weights;
            label14.Text = GetFormattedValue(weights[0]);   //Bias
            label20.Text = GetFormattedValue(weights[1]);
            label19.Text = GetFormattedValue(weights[2]);
            label25.Text = GetFormattedValue(weights[3]);   // Motor neighbour
            label23.Text = GetFormattedValue(weights[4]);   // Itself
            label33.Text = "T2: " + layer2.nodes[2].timeConstant;
            label34.Text = "G2: " + layer2.nodes[2].gain;

        }



    }
}
