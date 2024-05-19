using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BallPositionChange : EventArgs
    {
        public Vector2 Position { get; private set; }
        public int ID { get; private set; }

        public BallPositionChange(Vector2 position, int ID)
        {
            this.Position = position;
            this.ID = ID;
        }
    }
}
