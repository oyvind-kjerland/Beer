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
    public partial class Visualizer : Form
    {


        // Hardcoded level size
        private const int WORLD_WIDTH = 30;
        private const int WORLD_HEIGHT = 15;

        // Colors for the world, objects and tracker
        private Color EMPTY_COLOR = Color.White;
        private Color TRACKER_COLOR = Color.Blue;
        private Color SHADOW_COLOR = Color.DarkBlue;
        private Color BIG_COLOR = Color.Red;
        private Color SMALL_COLOR = Color.Green;

        // The Beer World
        private BeerWorld beerWorld;


        public Visualizer(BeerWorld beerWorld)
        {

            InitializeComponent();
            this.beerWorld = beerWorld;
            beerWorld.ResetWorld();
            InitializeGUI();
        }

        private void Visualizer_Load(object sender, EventArgs e)
        {
            backgroundWorker1.ProgressChanged += VisualizerUpdate;
        }



        public void InitializeGUI()
        {
            float colWidth = tableLayoutPanelGrid.Size.Width / (beerWorld.Width+1);
            float rowHeigth = tableLayoutPanelGrid.Size.Height / (beerWorld.Height+1);

            tableLayoutPanelGrid.RowCount = beerWorld.Height+1;
            tableLayoutPanelGrid.ColumnCount = beerWorld.Width+1;
            tableLayoutPanelGrid.RowStyles.Clear();
            tableLayoutPanelGrid.ColumnStyles.Clear();
            

            // Add row and column styles
            for (int i = 0; i < beerWorld.Height+1; i++)
            {
                tableLayoutPanelGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeigth));
            }

            for (int i = 0; i < beerWorld.Width+1; i++)
            {
                tableLayoutPanelGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, colWidth));
            }
            

            // Add picture boxes in each grid cell
            for (int y = 0; y < beerWorld.Height+1; y++)
            {
                for (int x = 0; x < beerWorld.Width+1; x++)
                {
                    PictureBox pb = new PictureBox();
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    if (y < beerWorld.Height && x < beerWorld.Width)
                    {
                        pb.BackColor = EMPTY_COLOR;
                    }
                    tableLayoutPanelGrid.Controls.Add(pb);
                }
            }


            // Fix margins
            foreach (Control control in tableLayoutPanelGrid.Controls)
            {
                control.Margin = new Padding(1);
            }
        }




        private void VisualizerDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgw = sender as BackgroundWorker;

            int timesteps = (int)numericTimesteps.Value;
            int interval = (int)numericInterval.Value;

            for (int i=0; i<timesteps; i++)
            {
                if (bgw.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                bgw.ReportProgress(0);
                System.Threading.Thread.Sleep(interval);
            }

        }



        

        private void VisualizerUpdate(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("Update");

            BeerObject[] beerObjects = { beerWorld.BeerObject, beerWorld.Tracker };
            PictureBox pb;
            int startX;
            Color color;

            int top = beerWorld.Top;

            // Clear the tracker and the beerObject from the current GUI
            color = EMPTY_COLOR;
            
            for (int y=0; y<beerWorld.Height; y++)
            {
                for (int x=0; x<beerWorld.Width; x++)
                {
                    pb = (PictureBox)tableLayoutPanelGrid.GetControlFromPosition(x, y);
                    if (pb.BackColor != color)
                    {
                        pb.BackColor = color;
                        pb.Refresh();
                    }
                }
            }
            


            // Update BeerWorld
            beerWorld.Update();
            beerObjects[0] = beerWorld.BeerObject;

            double[] sensors = null;

            // Update the GUI
            foreach (BeerObject beerObject in beerObjects)
            {
                if (beerObject == null) continue;

                startX = beerObject.X;

                // Set the correct color
                if (beerObject.IsTracker)
                {
                    color = TRACKER_COLOR;
                    sensors = ((BeerTracker)beerObject).Sensors;

                } else if (beerObject.IsBig)
                {
                    sensors = null;
                    color = BIG_COLOR;
                } else
                {
                    sensors = null;
                    color = SMALL_COLOR;
                }

                for (int x = startX; x < startX + beerObject.Width; x++)
                {
                    pb = (PictureBox)tableLayoutPanelGrid.GetControlFromPosition(x%beerWorld.Width, top-beerObject.Y);


                    if (sensors == null)
                    {
                        pb.BackColor = color;
                    } else
                    {
                        if (sensors[x-startX] >= 1)
                        {
                            pb.BackColor = SHADOW_COLOR;
                        } else
                        {
                            pb.BackColor = TRACKER_COLOR;
                        }
                    }


                    pb.Refresh();
                }
            }

            // Update labels

            labelHit.Text = beerWorld.ObjectsHit.ToString();
            labelAvoided.Text = beerWorld.ObjectsAvoided.ToString();
            labelCaught.Text = beerWorld.ObjectsCaught.ToString();
            labelMissed.Text = beerWorld.ObjectsMissed.ToString();
        }

        private void VisualizerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            beerWorld.NewSequence();
            backgroundWorker1.RunWorkerAsync();
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            beerWorld.MoveTracker(Beer.Move.LEFT, 1);
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            beerWorld.MoveTracker(Beer.Move.RIGHT, 1);
        }
    }
}
