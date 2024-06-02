using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    
    public class LogBall
    {
        public Vector2 Position;
        public Vector2 Speed;
        public long time;
        public LogBall(Vector2 position, Vector2 speed, long time)
        {
            Position = position;
            Speed = speed;
            this.time = time;
        }   
    }
}
