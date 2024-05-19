using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace Data
{
    public class Ball : IBallType
    {
        public Vector2 Speed { get; private set; }
        private Thread Thread;
        private int ID;
        private Table table;
        public int Mass { get; private set; }
        public int Radius { get; private set; }

        public event EventHandler<BallPositionChange> PropertyChanged;

        public Vector2 Position { get; private set; }

        public Ball(Vector2 Position, Vector2 movement, EventHandler<BallPositionChange> eventHandler, int ID, Table table)
        {
            this.PropertyChanged += eventHandler;
            this.Position = Position;
            this.Speed = movement;
            this.ID = ID;
            this.table = table;
            this.Mass = table.BallMass;
            this.Radius = table.BallRadius;
            this.Thread = new Thread(
                () =>
                {
                    while (true)
                    {
                        Move();
                        Thread.Sleep(15);
                    }
                });
            this.Thread.Start();
            this.Thread.IsBackground = true;
            
        }

        public void Move()
        {
            lock (this)
            {
                Vector2 newPosition = this.Position + this.Speed;
                // Bounce the ball
                if (newPosition.X < 0)
                {
                    newPosition = new Vector2(0, newPosition.Y);
                    this.Speed = new Vector2(-this.Speed.X, this.Speed.Y);
                }
                else if (newPosition.X + table.BallRadius > table.TableWidth)
                {
                    newPosition = new Vector2(table.TableWidth - table.BallRadius, newPosition.Y);
                    this.Speed = new Vector2(-this.Speed.X, this.Speed.Y);
                }
                if (newPosition.Y < 0)
                {
                    newPosition = new Vector2(newPosition.X, 0);
                    this.Speed = new Vector2(this.Speed.X, -this.Speed.Y);
                }
                else if (newPosition.Y + table.BallRadius > table.TableHeight)
                {
                    newPosition = new Vector2(newPosition.X, table.TableHeight - table.BallRadius);
                    this.Speed = new Vector2(this.Speed.X, -this.Speed.Y);
                }
                this.Position = newPosition;

                this.PropertyChanged.Invoke(this, new BallPositionChange(Position, ID));
            }
        }
        public void UpdateSpeed(Vector2 speed)
        {
            Speed = speed;
        }
    }
}
