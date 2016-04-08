using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Beer.EA;
using Beer.EA.DomainSpecific;
using System.Threading;
using System.Diagnostics;

namespace Beer
{
    public partial class Form1 : Form
    {

        private EALoop eaLoop;
        private BackgroundWorker bgw;

        private float max;
        private float average;
        private float sd;
        float[,] maxValues;
        float[,] averageValues;
        float[,] sdValues;

        private int currentSeries = 1;
        private string currentMaxString;
        private string currentAverageString;
        private string currentSdString;

        // Problem constants
        private const int CTRNN_INDEX = 0;

        // Adult selection constants
        private const int FULL_INDEX = 0;
        private const int OVER_INDEX = 1;
        private const int MIXING_INDEX = 2;

        // Parent selection constants
        private const int FITNESS_INDEX = 0;
        private const int SIGMA_INDEX = 1;
        private const int RANK_INDEX = 2;
        private const int TOURNAMENT_INDEX = 3;


        // Random object
        private Random random = new Random();
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxProblem.SelectedIndex = 0;
            comboBoxAdultSelector.SelectedIndex = 0;
            comboBoxParentSelector.SelectedIndex = TOURNAMENT_INDEX;



            Tuple<int, int> t = new Tuple<int, int>(1,2);
            Tuple<int, int> t2 = t;
            

        }


        private void SetupProblem()
        {
            if (comboBoxProblem.SelectedIndex == CTRNN_INDEX)
            {

                // Temp
                bool hasPull = true;

                // setup nodes
                int numSensorNodes = 5;
                int numMotorNodes = (hasPull) ? 3 : 2;
                int numLayers = 1;
                int[] numNodesPerLayer = new int[] { 2 };

                // Activation function is not used
                ActivationFunction activationfunction = null;
                ANN ann = new ANN(numSensorNodes, numMotorNodes, numLayers, numNodesPerLayer, activationfunction, true);

                int numWeights = ann.GetNumberOfWeights();
                int numGains = ann.GetNumberOfGains();
                int numTimeConstants = numGains;

                // Setup child population
                int childCount = (int)numericChildCount.Value;
                eaLoop.ChildCount = childCount;

                // Setup genotype
                int numBitsPerUnit = (int)numericBitsPerWeight.Value;

                int numBits = numBitsPerUnit * (numWeights + numGains + numTimeConstants);
                eaLoop.Genotype = new BinaryGenotype(numBits);

                // Setup phenotype developer
                BinaryToCTRNNWeightsDeveloper developer = new BinaryToCTRNNWeightsDeveloper();
                developer.NumBitsPerUnit = numBitsPerUnit;
                developer.NumGains = numGains;
                developer.NumTimeConstants = numTimeConstants;
                developer.NumWeights = numWeights;

                // Hardcoded :D
                developer.BiasIndices = (!hasPull) ? new int[] { 0, 6, 12, 15 } : new int[] { 0, 6, 12, 15, 18 };

                eaLoop.PhenotypeDeveloper = developer;

                // Setup Fitness evaluator
                BeerEvaluator evaluator = new BeerEvaluator();
                evaluator.BeerWorld.Tracker.ann = ann;
                evaluator.BeerWorld.WrapAround = true;

                // More hardcoding
                evaluator.TimeSteps = 600;

                eaLoop.FitnessEvaluator = evaluator;

                // Set genetic operator
                BinaryGeneticOperator op = new BinaryGeneticOperator();
                op.MutationRate = (float)mutationNumeric.Value;
                op.CrossoverRate = (float)crossoverNumeric.Value;
                eaLoop.GeneticOperator = op;


                eaLoop.goal = int.MaxValue;
            }
        }

        private void SetupAdultSelector()
        {
            switch(comboBoxAdultSelector.SelectedIndex)
            {
                // Full
                case FULL_INDEX:
                    eaLoop.AdultSelector = new FullSelector();
                    break;

                // Over Production
                case OVER_INDEX:
                    eaLoop.AdultSelector = new OverProductionSelector();                    
                    break;

                // Mixing
                case MIXING_INDEX:
                    eaLoop.AdultSelector = new MixingSelector();             
                    break;
            }
            eaLoop.AdultSelector.AdultCount = (int)numericAdultCount.Value;

            eaLoop.Elitism = checkBoxElitism.Checked;
        }

        private void SetupParentSelector()
        {
            switch (comboBoxParentSelector.SelectedIndex)
            {
                // Fitness-Proportionate
                case FITNESS_INDEX:
                    eaLoop.ParentSelector = new FitnessProportionate();
                    break;

                // Sigma-Scaling
                case SIGMA_INDEX:
                    eaLoop.ParentSelector = new SigmaScaling();
                    break;

                // Rank Scaling
                case RANK_INDEX:
                    RankScaling rankScaling = new RankScaling();
                    rankScaling.Min = (float)numericRankMin.Value;
                    rankScaling.Max = (float)numericRankMax.Value;
                    eaLoop.ParentSelector = rankScaling;
                    break;

                // Tournament Selector
                case TOURNAMENT_INDEX:
                    TournamentSelector tournamentSelector = new TournamentSelector();
                    tournamentSelector.K = (int)numericTournamentK.Value;
                    tournamentSelector.E = (float)numericTournamentE.Value;
                    eaLoop.ParentSelector = tournamentSelector;
                    break;
            }

            eaLoop.ParentSelector.ChildCount = (int)numericChildCount.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (bgw != null && bgw.IsBusy) return;

            // Instantiate EALoop
            eaLoop = new EALoop();

            // Setup the selected problem
            SetupProblem();

            // Set up adult selector and parent selector
            SetupAdultSelector();
            SetupParentSelector();

            // Start background worker
            if (bgw == null)
            {
                bgw = new BackgroundWorker();
                bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
                bgw.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
                bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            }

            bgw.WorkerReportsProgress = true;
            bgw.WorkerSupportsCancellation = true;
            
            // Clear the chart
            if (checkBoxClearOnRun.Checked)
            {
                ClearChart();
            }

            // Add a new series
            currentMaxString = "max_" + currentSeries.ToString();
            currentAverageString = "average_" + currentSeries.ToString();
            currentSdString = "sd_" + currentSeries.ToString();
            currentSeries++;

            

            if (checkBoxMax.Checked)
            {
                chart1.Series.Add(currentMaxString);
                
                chart1.Series[currentMaxString].ChartType = SeriesChartType.FastLine;
            }

            if (checkBoxAverage.Checked)
            {
                chart1.Series.Add(currentAverageString);
                chart1.Series[currentAverageString].ChartType = SeriesChartType.FastLine;
            }

            if (checkBoxSd.Checked)
            {
                chart1.Series.Add(currentSdString);
                chart1.Series[currentSdString].ChartType = SeriesChartType.FastLine;
            }

            // Set axis size
            chart1.ChartAreas[0].AxisX.Maximum = (int)numericNumGenerations.Value;
            
            
            // Run the background worker
            bgw.RunWorkerAsync();

        }

        // Problem ComboBox
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            
            BackgroundWorker worker = sender as BackgroundWorker;

            // Get settings
            int numGenerations = (int)numericNumGenerations.Value;
            int numRuns = (int)numericNumRuns.Value;

            maxValues =  new float[numGenerations, numRuns];
            averageValues = new float[numGenerations, numRuns];
            sdValues = new float[numGenerations, numRuns];
            bool plotSd = checkBoxSd.Checked;
            
            for (int n=0; n<numRuns; n++)
            {
                eaLoop.Initialize();
                for (int i = 0; i < numGenerations; i++)
                {
                    if (worker.CancellationPending) return;

                    // Perform one iteration of the loop
                    eaLoop.Iterate();

                    // Sample data
                    max = eaLoop.max;
                    average = eaLoop.average;
                    maxValues[i, n] = max;
                    averageValues[i, n] = average;

                    
                    // Calculate standard deviations
                    sd = 0;
                    foreach (Individual individual in eaLoop.AdultPopulation)
                    {
                        sd += (float)Math.Pow(individual.Fitness - average, 2);
                    }
                    sd /= eaLoop.AdultPopulation.Count;
                    sd = (float)Math.Sqrt(sd);
                    sdValues[i, n] = sd;
                    

                    if (numRuns == 1)
                    {
                        worker.ReportProgress(i);
                        if (max == eaLoop.goal)
                        {
                            Debug.WriteLine("Reached goal");
                            return;
                        }
                    }


                }
            }

            // Find the maximal individual
            Debug.WriteLine(max);
            return;
        }

        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (checkBoxMax.Checked)
            {
                chart1.Series[currentMaxString].Points.Add(max);
            } 

            if (checkBoxAverage.Checked)
            {
                chart1.Series[currentAverageString].Points.Add(average);
            }

            if (checkBoxSd.Checked)
            {
                chart1.Series[currentSdString].Points.Add(sd);
            }

            Debug.WriteLine("gen: " + eaLoop.generation.ToString());
            Debug.WriteLine("max: " + max.ToString());
            Debug.WriteLine("average: " + average.ToString());
            Debug.WriteLine("sd: " + sd.ToString());
            if (eaLoop.best != null)
            {
                Debug.WriteLine("best: " + eaLoop.best.Phenotype.GetPhenotypeString());
            }
            
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (maxValues == null || averageValues == null) return;
            if ((int)numericNumRuns.Value == 1) return; 

            // Calculate the average of the max and average values and plot thems
            float maxSum, averageSum, sdSum;
            float[] max = new float[maxValues.GetLength(0)];
            float[] average = new float[averageValues.GetLength(0)];
            float[] sd = new float[sdValues.GetLength(0)];

            for (int i=0; i<maxValues.GetLength(0); i++)
            {
                maxSum = 0;
                averageSum = 0;
                sdSum = 0;
                for (int n = 0; n < maxValues.GetLength(1); n++)
                {
                    maxSum += maxValues[i, n];
                    averageSum += averageValues[i, n];
                    sdSum += sdValues[i, n];
                }

                max[i] = maxSum / maxValues.GetLength(1);
                average[i] = averageSum / maxValues.GetLength(1);
                sd[i] = sdSum / maxValues.GetLength(1);
            }


            // Plot it
            if (checkBoxMax.Checked)
            {
                for (int i=0; i<max.Length; i++)
                {
                    chart1.Series[currentMaxString].Points.Add(max[i]);
                }
            }

            if (checkBoxAverage.Checked)
            {
                for (int i=0; i<average.Length; i++)
                {
                    chart1.Series[currentAverageString].Points.Add(average[i]);
                }
            }

            if (checkBoxSd.Checked)
            {
                for (int i = 0; i < average.Length; i++)
                {
                    chart1.Series[currentSdString].Points.Add(sd[i]);
                }
            }

        }

        private void crossoverNumeric_ValueChanged(object sender, EventArgs e)
        {

        }

        private void mutationNumeric_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void numericAdultCount_ValueChanged(object sender, EventArgs e)
        {
            numericChildCount.Minimum = numericAdultCount.Value;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxParentSelector_SelectedIndexChanged(object sender, EventArgs e)
        {

            bool tournamentStatus = (comboBoxParentSelector.SelectedIndex == TOURNAMENT_INDEX);

            labelTournamentK.Enabled = tournamentStatus;
            labelTournamentE.Enabled = tournamentStatus;
            numericTournamentK.Enabled = tournamentStatus;
            numericTournamentE.Enabled = tournamentStatus;

            bool rankStatus = (comboBoxParentSelector.SelectedIndex == RANK_INDEX);
            labelRankMin.Enabled = rankStatus;
            labelRankMax.Enabled = rankStatus;
            numericRankMin.Enabled = rankStatus;
            numericRankMax.Enabled = rankStatus;

        }

        // Cancel button
        private void button1_Click_1(object sender, EventArgs e)
        {
            bgw.CancelAsync();
        }

        // Clear button
        private void button2_Click(object sender, EventArgs e)
        {
            ClearChart();
        }

        private void ClearChart()
        {
            currentSeries = 1;
            chart1.Series.Clear();
        }

        private void comboBoxAdultSelector_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // Do nothing if no layer is selected
            if (listBoxANN.SelectedIndex < 0) return;

            listBoxANN.Items[listBoxANN.SelectedIndex] = (int)numericNumNodes.Value;
        }

        private void buttonAddLayer_Click(object sender, EventArgs e)
        {
            // Add a new layer
            listBoxANN.Items.Add((int)numericNumNodes.Value);
        }

        private void buttonRemoveLayer_Click(object sender, EventArgs e)
        {
            // Do nothing if no layer is selected
            if (listBoxANN.SelectedIndex < 0) return;

            // Remove the selected index
            listBoxANN.Items.RemoveAt(listBoxANN.SelectedIndex);
        }

        private void buttonShowSimulation_Click(object sender, EventArgs e)
        {
            BeerWorld beerWorld = ((BeerEvaluator)eaLoop.FitnessEvaluator).BeerWorld;
            Visualizer visualizer = new Visualizer(beerWorld);
            visualizer.ShowDialog();
        }
    }
}
