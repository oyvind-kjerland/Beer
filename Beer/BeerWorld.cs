using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer
{

    public class BeerWorld
    {

        // Hardcode tracker size and object sizes
        private const int TRACKER_WIDTH = 5;
        private const int MIN_OBJ_WIDTH = 1;
        private const int MAX_OBJ_WIDTH = 6;


        public int Width { get; private set; }
        public int Height { get; private set; }

        public int Bottom { get; private set; }
        public int Top { get; private set; }
        public int CenterX { get; private set; }

        public BeerObject BeerObject { get; private set; }
        public BeerTracker Tracker { get; set; }


        // Random
        private Random random;

        // Stats
        public int ObjectsAvoided;
        public int ObjectsCaught;
        public int ObjectsHit;
        public int ObjectsMissed;


        // Wrap around world
        public bool WrapAround;

        // Sequence (x,width)
        private List<int[]> sequence;
        private int sequenceIndex;
        public int NumObjects = 45;


        public BeerWorld(int width, int height)
        {
            // Init width and height
            this.Width = width;
            this.Height = height;

            this.Bottom = 0;
            this.Top = height - 1;

            // Initialize new random object
            random = new Random();


            // Init tracker
            Tracker = new BeerTracker(TRACKER_WIDTH);

        }

        public void NewSequence()
        {

            int x, width;
            sequence = new List<int[]>();


            for (int i = 0; i < NumObjects; i++)
            {
                // Calculate a random width
                width = random.Next(MIN_OBJ_WIDTH, MAX_OBJ_WIDTH + 1);

                // Set a random position
                x = random.Next(this.Width + 1 - width);

                sequence.Add(new int[2]{x,width});
            }

        }

        public void ResetWorld()
        {

            // Setup stats
            ObjectsAvoided = 0;
            ObjectsCaught = 0;
            ObjectsHit = 0;
            ObjectsMissed = 0;

            // Reset sequence
            sequenceIndex = 0;

            SpawnObject();
            Tracker.SetPosition(CenterX, Bottom);

        }

        public void Update()
        {

            // Check if there is a beer object
            if (BeerObject == null)
            {
                SpawnObject();
                UpdateSensors();
            }

            // Move object
            BeerObject.Y--;

            // Check if the object is at the same level as the tracker
            if (BeerObject.Y == Bottom)
            {
                // Big Beer object
                if (BeerObject.IsBig)
                {
                    if (BeerObject.Left > Tracker.Right || BeerObject.Right < Tracker.Left)
                    {
                        ObjectsAvoided++;
                    } else
                    {
                        ObjectsHit++;
                    }

                // Small Beer object
                } else
                {
                    // Check capture
                    if (BeerObject.Left >= Tracker.Left && BeerObject.Right <= Tracker.Right)
                    {
                        ObjectsCaught++;
                    } else
                    {
                        ObjectsMissed++;
                    }
                }


                // Remove object
                BeerObject = null;
            }

            // Move tracker
            Tracker.GetMove(Tracker.Sensors);
            MoveTracker(Tracker.currentMove, Tracker.currentSpeed);

            // Obtain sensor data
            UpdateSensors();
        }

        public void UpdateSensors()
        {
            double[] senseUp = new double[TRACKER_WIDTH];
            if (BeerObject != null)
            {
                for (int x = 0; x < Tracker.Width; x++)
                {
                    if (x + Tracker.X >= BeerObject.Left && x + Tracker.X < BeerObject.Right)
                    {
                        senseUp[x] = 1;
                    }
                    else
                    {
                        senseUp[x] = 0;
                    }
                }
            }

            Tracker.Sensors = senseUp;
        }

        public void MoveTracker(Move move, int speed)
        {
            Tracker.X += (int)move * speed;

            if (WrapAround) Tracker.X = Utility.Mod(Tracker.X, Width);


            // Check bounds
            if (!WrapAround)
            {
                if (Tracker.X < 0)
                {
                    Tracker.X = 0;
                }
                else if (Tracker.Right > Width )
                {
                    Tracker.X -= 1;
                }
            }
        }

        public void SpawnObject()
        {
            int[] data = sequence[sequenceIndex];
            int x = data[0];
            int width = data[1];

            // Create a new BeerObject
            BeerObject = new BeerObject(width, TRACKER_WIDTH);
            BeerObject.SetPosition(x, Top);

            sequenceIndex++;
        }

    }
}
