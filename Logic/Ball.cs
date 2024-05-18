using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace Logic
{
    public class Ball : IBallType
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public (double moveX,double moveY) Direction { get; private set; }
        private Thread Thread;

        public Ball(double x, double y, (double moveX, double moveY) Direction)
        {
            X = x;
            Y = y;
            this.Direction = Direction;
            this.Thread = new Thread(
                () =>
                {
                    while (true)
                    {
                        Move(10, 600, 300);
                    }
                });
            this.Thread.Start();
            this.Thread.IsBackground = true;
        }

        public void Move(int radius, int maxX, int maxY)
        {

            this.X += this.Direction.moveX;
            this.Y += this.Direction.moveY;
            // Bounce the ball
            if (this.X < 0)
            {
                this.X = 0;
                this.Direction = (-this.Direction.moveX, this.Direction.moveY);
            }
            if (this.X + radius > maxX)
            {
                this.X = maxX - radius;
                this.Direction = (-this.Direction.moveX, this.Direction.moveY);
            }
            if (this.Y < 0)
            {
                this.Y = 0;
                this.Direction = (this.Direction.moveX, -this.Direction.moveY);
            }
            if (this.Y + radius > maxY)
            {
                this.Y = maxY - radius;
                this.Direction = (this.Direction.moveX, -this.Direction.moveY);
            }

        }
    }
}
