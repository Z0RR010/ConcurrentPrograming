using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class PositionUpdateArgs : EventArgs
    {
        public Vector2 Position { get; set; }
        public int ID { get; set; }

        public PositionUpdateArgs(Vector2 position, int iD)
        {
            Position = position;
            ID = iD;
        }
    }
}
