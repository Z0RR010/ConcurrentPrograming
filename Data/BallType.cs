using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IBallType
    {
        public Vector2 Position { get; }
        public Vector2 Movement { get; }


        public abstract void Move(int radius, int maxX, int maxY);

    }
}
