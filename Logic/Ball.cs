using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Ball
    {
        public double X { get; set; }
        public double Y { get; set; }
        public (double moveX,double moveY) Direction { get; set; }

        public Ball(double x, double y, (double moveX, double moveY) Direction)
        {
            this.X = x;
            this.Y = y;
            this.Direction = Direction;
        }
    }
}
