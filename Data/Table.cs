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
        public int BallRadius { get
            {
                return random.Next(5, 15);
            }}
        public int BallMass { get
            {
                return random.Next(5, 15);
            }
        }

        private Random random = new Random();

        public Table(int height = 300, int width = 600)
        {
            
            TableHeight = height;
            TableWidth = width;
        }
    }
}
