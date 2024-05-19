using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Table
    {
        public int TableHeight { get; private set; }
        public int TableWidth { get; private set; }
        public int BallRadius { get; private set; }
        public int BallMass { get; private set; }

        public Table(int height = 300, int width = 600, int radius = 10, int ballMass = 10)
        {
            TableHeight = height;
            TableWidth = width;
            BallRadius = radius;
            BallMass = ballMass;
        }
    }
}
