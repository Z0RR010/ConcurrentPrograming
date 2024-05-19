using System.Collections;
using System.Collections.ObjectModel;
using Timer = System.Timers.Timer;
using Data;
using System.Runtime.CompilerServices;
using System.Numerics;
using System.ComponentModel;
using System.Diagnostics;

namespace Logic
{ 

    internal class LogicApi : LogicAbstractApi
    {
        DataAbstractApi dataApi;

        private ICollection<IBallType> balls;

        private EventHandler<PositionUpdateArgs> updatehandler;

        private Table table;

        private Thread CollisionChecking;


        public LogicApi(DataAbstractApi data) 
        {
            this.dataApi = data;
            this.balls = dataApi.GetRepository<IBallType>();
            this.table = new Table();
        }

        public override ICollection<IBallType> CreateRepository()
        {
            return this.dataApi.GetRepository<IBallType>();
        }

        public override void GenerateHandler(int ballsNumber, EventHandler<PositionUpdateArgs> eventHandler)
        {
            this.updatehandler = eventHandler;
            var randomGenerator = new Randomizer();
            if (this.balls.Count != 0 || ballsNumber == 0) return;
            foreach (int i in Enumerable.Range(0,ballsNumber))
            {
                var newBall = this.dataApi.GetBall(new Vector2(randomGenerator.GenerateFloat(0, table.TableWidth - table.BallRadius), randomGenerator.GenerateFloat(0, table.TableHeight - table.BallRadius)), randomGenerator.GenerateVector(), HandleBallUpdates, i, table);
                this.balls.Add(newBall);
            }
            this.CollisionChecking = new Thread(() =>
            {
                while (true)
                {
                    CheckBallCollisions();
                    Thread.Sleep(3);
                }
            });
            CollisionChecking.IsBackground = true;
            CollisionChecking.Start();
        }

        public override void Stop(Timer timer)
        {
            timer.Enabled = false;
        }

        void HandleBallUpdates(object? ball, BallPositionChange Position)
        {
            int id = Position.ID;
            Vector2 pos = Position.Position;
            updatehandler.Invoke(this, new PositionUpdateArgs(pos, id));
        }


        private void CheckBallCollisions()
        {
            foreach (IBallType st in balls)
            {
                Vector2 pos1 = st.Position;
                foreach (IBallType nd in balls)
                {
                    Vector2 pos2 = nd.Position;
                    float dist = Vector2.Distance(pos1, pos2);
                    if (st != nd && dist <= (st.Radius /2 + nd.Radius /2 ))
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

        public override List<Vector2> GetBallPositions()
        {
            List<Vector2> ret = new List<Vector2>();
            foreach (var ball in this.balls) 
            {
                ret.Add(ball.Position);
            }
            return ret;
        }
    }

}