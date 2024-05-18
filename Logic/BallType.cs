using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IBallType
    {
        public double X { get; }
        public double Y { get; }
        public (double moveX, double moveY) Direction { get; }


        public abstract void Move(int radius, int maxX, int maxY);

    }
}
