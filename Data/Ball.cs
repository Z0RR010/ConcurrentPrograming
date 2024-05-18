using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;

namespace Data
{
    public class Ball : IBallType
    {
        public Vector2 Movement { get; private set; }
        private Thread Thread;

        public event EventHandler<BallPositionChange> PropertyChanged;

        public Vector2 Position { get; private set; }

        public Ball(Vector2 Position, Vector2 movement, EventHandler<BallPositionChange> eventHandler)
        {
            this.PropertyChanged += eventHandler;
            this.Position = Position;
            this.Movement = movement;
            
            this.Thread = new Thread(
                () =>
                {
                    while (true)
                    {
                        Move(10, 600, 300);
                        Thread.Sleep(25);
                    }
                });
            this.Thread.Start();
            this.Thread.IsBackground = true;
            
        }

        public void Move(int radius, int maxX, int maxY)
        {

            Vector2 newPosition =  this.Position + this.Movement;
            // Bounce the ball
            if (newPosition.X < 0)
            {
                newPosition = new Vector2(0,newPosition.Y);
                this.Movement = new Vector2(-this.Movement.X, this.Movement.Y);
            }
            else if (newPosition.X + radius > maxX)
            {
                newPosition = new Vector2(maxX - radius, newPosition.Y);
                this.Movement = new Vector2(-this.Movement.X, this.Movement.Y);
            }
            if (newPosition.Y < 0)
            {
                newPosition = new Vector2(newPosition.X,0);
                this.Movement = new Vector2(this.Movement.X, -this.Movement.Y);
            }
            else if (newPosition.Y + radius > maxY)
            {
                newPosition = new Vector2(newPosition.X,maxY - radius);
                this.Movement = new Vector2(this.Movement.X, -this.Movement.Y);
            }
            this.Position = newPosition;

            this.PropertyChanged.Invoke(this, new BallPositionChange(this.Position));
        }
    }
}
