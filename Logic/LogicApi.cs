using System.Collections;
using System.Collections.ObjectModel;
using Timer = System.Timers.Timer;
using Data;
using System.Runtime.CompilerServices;
using System.Numerics;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace Logic
{ 

    internal class LogicApi : LogicAbstractApi
    {
        DataAbstractApi dataApi;

        private ICollection<IBallType> balls;

        private Table table;

        private Thread CollisionChecking;

        private readonly Logger logger;

        public LogicApi(DataAbstractApi data) 
        {
            this.dataApi = data;
            this.balls = dataApi.GetRepository<IBallType>();
            this.table = dataApi.GetTable();//GetTable() sprawdzić czy ok
        }

        public override ICollection<IBallType> CreateRepository()
        {
            return this.dataApi.GetRepository<IBallType>();
        }

        public override void GenerateHandler(int number)
        {
            //this.updatehandler = eventHandler;
            var randomGenerator = new Randomizer();
            if (this.balls.Count != 0 || number == 0)
            {
                return;
            }

            lock (balls)
            {
                foreach (var i in Enumerable.Range(0,number))
                {
                    var newBall = this.dataApi.GetBall(new Vector2(randomGenerator.GenerateFloat(0, table.TableWidth), randomGenerator.GenerateFloat(0, table.TableHeight)), randomGenerator.GenerateVector(), table);
                    this.balls.Add(newBall);
                }
                this.CollisionChecking = new Thread(() =>
                {
                    while (true)
                    {
                        CheckBallCollisions();
                    }
                });
                CollisionChecking.IsBackground = true;
                CollisionChecking.Start();
            }
        }

        public override void ConnectBalls(List<EventHandler<ReadOnlyCollection<float>>> eventHandlers)
        {
            foreach (int i in Enumerable.Range(0,balls.Count))
            {
                balls.ElementAt(i).Connect(eventHandlers[i]);
                balls.ElementAt(i).Start();
            }
        }

        public override void Stop()
        {
            lock (balls)
            {
                foreach (IBallType ball in balls)
                {
                    ball.Stop();
                }
                balls.Clear();
            }
        }

        //void HandleBallUpdates(object? ball, BallPositionChange Position)
        //{
        //    Vector2 pos = Position.Position;
        //    //updatehandler.Invoke(this, new PositionUpdateArgs(pos));
        //}


        private void CheckBallCollisions()
        {
            lock (balls)
            {
                foreach (IBallType st in balls)
                {
                    Vector2 pos1 = st.Position;
                    foreach (IBallType nd in balls)
                    {
                        Vector2 pos2 = nd.Position;
                        float dist = Vector2.Distance(pos1, pos2);
                        if (st != nd && dist <= (st.Radius / 2 + nd.Radius / 2) && dist > Vector2.Distance(pos1 + st.Speed, pos2 + nd.Speed))
                        {
                            lock (st) lock (nd)
                                {
                                    float stBallXSpeed = st.Speed.X * (st.Mass - nd.Mass) / (st.Mass + nd.Mass)
                                                       + nd.Mass * nd.Speed.X * 2f / (st.Mass + nd.Mass);
                                    float stBallYSpeed = st.Speed.Y * (st.Mass - nd.Mass) / (st.Mass + nd.Mass)
                                                           + nd.Mass * nd.Speed.Y * 2f / (st.Mass + nd.Mass);

                                    float ndballXSpeed = nd.Speed.X * (nd.Mass - st.Mass) / (nd.Mass + nd.Mass)
                                                      + st.Mass * st.Speed.X * 2f / (nd.Mass + st.Mass);
                                    float ndBallYSpeed = nd.Speed.Y * (nd.Mass - st.Mass) / (nd.Mass + nd.Mass)
                                                      + st.Mass * st.Speed.Y * 2f / (nd.Mass + st.Mass);

                                    st.UpdateSpeed(new Vector2(stBallXSpeed, stBallYSpeed));
                                    nd.UpdateSpeed(new Vector2(ndballXSpeed, ndBallYSpeed));
                                }
                        }
                    }
                }
            }
        }

        public override (List<Vector2>, List<int>) GetBallInfo()
        {
            List<Vector2> ret = new List<Vector2>();
            List<int> radius = new List<int>();
            foreach (var ball in this.balls) 
            {
                ret.Add(ball.Position);
                radius.Add(ball.Radius);
            }
            return (ret, radius);
        }
    }

}