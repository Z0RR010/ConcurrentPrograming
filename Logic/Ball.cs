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
        public double direction { get; set; }

        public Ball(double x, double y, double direction)
        {
            this.X = x;
            this.Y = y;
            this.direction = direction;
        }
    }
}
