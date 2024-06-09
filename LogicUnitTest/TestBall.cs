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
        //private int ID;
        private Table table;
        public int Mass { get; private set; }
        public int Radius { get; private set; }
        private bool run = true;


        public Vector2 Position { get; private set; }

        public TestBall(Vector2 Position, Vector2 movement,  Table table) //int ID,
        {
            this.Position = Position;
            this.Speed = movement;
            //this.ID = ID;
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

        public void Connect(EventHandler<ReadOnlyCollection<float>> eventHandler)
        {
            throw new NotImplementedException();
        }

    }
}

    //public class TestBall : IBallType
    //{
    //    private Thread Thread;
    //    private Table table;
    //    private bool run = true;
    //    private Stopwatch stopwatch = new Stopwatch();
    //    private long LastTime = 0;
    //    private readonly Logger? logger;
    //    public Vector2 Position { get; private set; }
    //    public Vector2 Speed { get; private set; }
    //    public int Mass { get; private set; }
    //    public int Radius { get; private set; }

    //    private EventHandler<ReadOnlyCollection<float>> EventHandler;
    //    private readonly Object lockObject = new Object();


    //    public TestBall(Vector2 position, Vector2 movement, Table table)
    //    {
    //        this.Position = position;
    //        this.Speed = movement;
    //        this.table = table;
    //        this.Mass = table.BallMass;
    //        this.Radius = table.BallRadius;
    //        stopwatch.Start();
    //        this.Thread = new Thread(
    //            () =>
    //            {
    //                while (run)
    //                {
    //                    long time = stopwatch.ElapsedTicks;
    //                    long elapsed = time - LastTime;
    //                    LastTime = time;
    //                    Move(elapsed);
    //                    Thread.Sleep(25);
    //                }
    //            });
    //        this.Thread.IsBackground = true;
    //    }

    //    public void Start()
    //    {
    //        this.Thread.Start();
    //    }

    //    public void Stop()
    //    {
    //        run = false;
    //    }

    //    public void Connect(EventHandler<ReadOnlyCollection<float>> eventHandler)
    //    {
    //        this.EventHandler = eventHandler;
    //    }

    //    public void UpdateSpeed(Vector2 speed)
    //    {
    //        lock (lockObject)
    //        {
    //            this.Speed = speed;
    //        }

    //    }

    //    private void Move(long time)
    //    {
    //        //lock (this)
    //        //{
    //        //    Vector2 newPosition = this.Position + (this.Speed * time / 60000);

    //        //    if (newPosition.X < 0)
    //        //    {
    //        //        newPosition = new Vector2(0, newPosition.Y);
    //        //        this.Speed = new Vector2(-this.Speed.X, this.Speed.Y);
    //        //    }
    //        //    else if (newPosition.X + Radius > table.TableWidth)
    //        //    {
    //        //        newPosition = new Vector2(table.TableWidth - Radius, newPosition.Y);
    //        //        this.Speed = new Vector2(-this.Speed.X, this.Speed.Y);
    //        //    }
    //        //    if (newPosition.Y < 0)
    //        //    {
    //        //        newPosition = new Vector2(newPosition.X, 0);
    //        //        this.Speed = new Vector2(this.Speed.X, -this.Speed.Y);
    //        //    }
    //        //    else if (newPosition.Y + Radius > table.TableHeight)
    //        //    {
    //        //        newPosition = new Vector2(newPosition.X, table.TableHeight - Radius);
    //        //        this.Speed = new Vector2(this.Speed.X, -this.Speed.Y);
    //        //    }
    //        //    this.Position = newPosition;

    //        //    List<float> pos = new()
    //        //    {
    //        //        newPosition.X,
    //        //        newPosition.Y
    //        //    };

    //        //    ReadOnlyCollection<float> position = new(pos);
    //        //    this.EventHandler.Invoke(this, position);
    //        //}
    //    }
