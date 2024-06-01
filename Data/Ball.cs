﻿using System;
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
        private Table table;
        public int Mass { get; private set; } //useless
        public int Radius { get; private set; } //useless
        private bool run = true;

        public event EventHandler<BallPositionChange> PropertyChanged; //private

        public Vector2 Position { get; private set; }

        public Ball(Vector2 Position, Vector2 movement, EventHandler<BallPositionChange> eventHandler, Table table)
        {
            this.PropertyChanged += eventHandler;
            this.Position = Position;
            this.Speed = movement;
            this.table = table;
            this.Mass = table.BallMass;
            this.Radius = table.BallRadius;
            this.Thread = new Thread( //Argument = delegate void;
                () =>
                {
                    while (run)
                    {
                        Move();
                        Thread.Sleep(25); //to jest zmiana stanu w suspended, żeby potem przeszedł w ready
                    }                     //do zadanie 3) użyć stopwatch, pomierzyć czas i go przekazać jako parametr
                });                       //        //IDisposable - coś do niszczenia wątków. Użyć dispose().
            this.Thread.IsBackground = true;
            
        }
        //Vector2 nie jest immutable bo X i Y można zmienić.
        public void Start()
        {
            this.Thread.Start();
        }

        public void Stop() 
        {
            run = false;
        }

        public void Move() //private
        {
            lock (this)
            {
                Vector2 newPosition = this.Position + this.Speed; //speed*1f będzie czas rzeczywisty.
                // Bounce the ball
                if (newPosition.X < 0)
                {
                    newPosition = new Vector2(0, newPosition.Y);
                    this.Speed = new Vector2(-this.Speed.X, this.Speed.Y);
                }
                else if (newPosition.X + Radius > table.TableWidth)
                {
                    newPosition = new Vector2(table.TableWidth - Radius, newPosition.Y);
                    this.Speed = new Vector2(-this.Speed.X, this.Speed.Y);
                }
                if (newPosition.Y < 0)
                {
                    newPosition = new Vector2(newPosition.X, 0);
                    this.Speed = new Vector2(this.Speed.X, -this.Speed.Y);
                }
                else if (newPosition.Y + Radius > table.TableHeight)
                {
                    newPosition = new Vector2(newPosition.X, table.TableHeight - Radius);
                    this.Speed = new Vector2(this.Speed.X, -this.Speed.Y);
                }
                this.Position = newPosition;

                this.PropertyChanged.Invoke(this, new BallPositionChange(Position));
            }
        }
        public void UpdateSpeed(Vector2 speed)
        {
            Speed = speed;
        }
    }
}
