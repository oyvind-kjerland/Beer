using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer
{
    public class BeerObject
    {
        public int Width { get; private set; }
        protected int x;
        public int X {
            get { return x; }
            set
            {
                x = value;
                this.Left = X;
                this.Right = X + Width;
            }
        }
        public int Y { get; set; }
        public int Left { get; protected set; }
        public int Right { get; protected set; }
        public bool IsTracker { get; protected set; }
        public bool IsBig { get; private set; }

        public BeerObject(int width)
        {
            this.Width = width;
            this.IsTracker = false;
        }

        public BeerObject(int width, int bigThreshold)
        {
            this.Width = width;
            this.IsTracker = false;
            this.IsBig = (Width >= bigThreshold);
        }



        public void SetPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
