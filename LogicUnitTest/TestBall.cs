using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Data;
namespace LogicUnitTest
{
    public class TestBall : IBallType
    {
        public Vector2 Speed { get; private set; }
        private Thread Thread;
        private int ID;
        private Table table;
        public int Mass { get; private set; }
        public int Radius { get; private set; }
        private bool run = true;


        public Vector2 Position { get; private set; }

        public TestBall(Vector2 Position, Vector2 movement, int ID, Table table)
        {
            this.Position = Position;
            this.Speed = movement;
            this.ID = ID;
            this.table = table;
            this.Mass = table.BallMass;
            this.Radius = table.BallRadius;
            this.Thread = new Thread(
                () =>
                {
                    while (run)
                    {
                        Move();
                        Thread.Sleep(25);
                    }
                });
            this.Thread.IsBackground = true;
            
        }

        public void Start()
        {
            this.Thread.Start();
        }

        public void Stop() 
        {
            run = false;
        }

        public void Move()
        {
            
        }

        public void UpdateSpeed(Vector2 speed)
        {
            Speed = speed;
        }

        public void Connect(EventHandler<Vector2> eventHandler)
        {
            throw new NotImplementedException();
        }

        public void Connect(EventHandler<ReadOnlyCollection<float>> eventHandler)
        {
            throw new NotImplementedException();
        }
    }
}
