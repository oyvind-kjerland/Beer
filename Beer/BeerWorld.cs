﻿using System;
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

        // Has pulled on this timestep
        public bool hasPulled;

        // Sequence (x,width)
        private List<int[]> sequence;
        private int sequenceIndex;
        public int NumObjects = 600;


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
            sequenceIndex = 0;

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
            // Reset has pulled
            hasPulled = false;

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
                CheckHit();

                // Remove object
                BeerObject = null;
            }

            // Find next move
            Tracker.GetMove(Tracker.Sensors);

            if (Tracker.currentMove == Move.PULL)
            {
                // Pull
                Pull();
            } else
            {
                // Move tracker
                MoveTracker(Tracker.currentMove, Tracker.currentSpeed);
            }

            // Obtain sensor data
            UpdateSensors();
        }

        public void Pull()
        {
            CheckHit();
            BeerObject = null;
            hasPulled = true;
        }

        // Check if the tracker hits the beerObject
        public void CheckHit()
        {
            if (BeerObject == null) return;

            // Big Beer object
            if (BeerObject.IsBig)
            {
                if (BeerObject.Left > Tracker.Right || BeerObject.Right < Tracker.Left)
                {
                    ObjectsAvoided++;
                }
                else
                {
                    ObjectsHit++;
                }

            // Small Beer object
            }
            else
            {
                // Check capture
                if (BeerObject.Left >= Tracker.Left && BeerObject.Right <= Tracker.Right)
                {
                    ObjectsCaught++;
                }
                else
                {
                    ObjectsMissed++;
                }
            }
        }

        public void UpdateSensors()
        {
            
            double[] senseUp = (WrapAround) ? new double[TRACKER_WIDTH] : new double[TRACKER_WIDTH+2];

            if (BeerObject != null)
            {
                for (int x = 0; x < Tracker.Width; x++)
                {
                    if ((x + Tracker.X)%Width >= BeerObject.Left && (x + Tracker.X)%Width < BeerObject.Right)
                    {
                        senseUp[x] = 1;
                    }
                    else
                    {
                        senseUp[x] = 0;
                    }
                }
            }

            // Add sensors for left and right edges
            if (!WrapAround)
            {
                // Sense left
                senseUp[TRACKER_WIDTH] = (Tracker.Left == 0) ? 1 : 0;
                // Sense right
                senseUp[TRACKER_WIDTH + 1] = (Tracker.Right == this.Width) ? 1 : 0;
            } 

            Tracker.Sensors = senseUp;
        }

        public void MoveTracker(Move move, int speed)
        {
            Tracker.X += (int)move * speed;

            if (WrapAround)
            {
                Tracker.X = Utility.Mod(Tracker.X, Width);
            }
            else
            {
                if (Tracker.X < 0)
                {
                    Tracker.X = 0;
                }
                else if (Tracker.Right >= Width)
                {
                    Tracker.X = Width - Tracker.Width;
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
