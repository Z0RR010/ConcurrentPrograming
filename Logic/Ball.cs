using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Ball : IBallType
    {
        public double X { get; set; }
        public double Y { get; set; }
        public (double moveX,double moveY) Direction { get; set; }

        public Ball(double x, double y, (double moveX, double moveY) Direction)
        {
            X = x;
            Y = y;
            this.Direction = Direction;
        }

        public void Move(int radius, int maxX, int maxY)
        {
   
            var newBall = new Ball(this.X + this.Direction.moveX, this.Y + this.Direction.moveY, this.Direction);
            // Bounce the ball
            if (newBall.X < 0) newBall = new Ball(0, newBall.Y, (-this.Direction.moveX, this.Direction.moveY));
            if (newBall.X + radius > maxX) newBall = new Ball(maxX - radius, newBall.Y, (-this.Direction.moveX, this.Direction.moveY));
            if (newBall.Y < 0) newBall = new Ball(newBall.X, 0, (this.Direction.moveX, -this.Direction.moveY));
            if (newBall.Y + radius > maxY) newBall = new Ball(newBall.X, maxY - radius, (this.Direction.moveX, -this.Direction.moveY));

            this.Direction = newBall.Direction;
            this.X = newBall.X;
            this.Y = newBall.Y;
        }
    }
}
