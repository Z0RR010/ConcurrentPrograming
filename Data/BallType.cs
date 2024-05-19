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
        public Vector2 Speed { get; }
        public int Mass { get; }
        public int Radius { get; }


        public abstract void Move();
        public abstract void Start();
        public abstract void Stop();

        public abstract void UpdateSpeed(Vector2 speed);


    }
}
