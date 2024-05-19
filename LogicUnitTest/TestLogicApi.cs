using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Data;
using Logic;

namespace LogicUnitTest
{
    internal class TestLogicApi : LogicAbstractApi
    {
        DataAbstractApi dataApi;

        private ICollection<IBallType> balls;

        private EventHandler<PositionUpdateArgs> updatehandler;

        private Table table;

        private Thread CollisionChecking;

        public TestLogicApi(DataAbstractApi data)
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
            if (this.balls.Count != 0 || ballsNumber == 0)
            {
                return;
            }
            lock (balls)
            {
                foreach (int i in Enumerable.Range(0, ballsNumber))
                {
                    var newBall = this.dataApi.GetBall(new Vector2(randomGenerator.GenerateFloat(0, table.TableWidth - table.BallRadius), randomGenerator.GenerateFloat(0, table.TableHeight - table.BallRadius)), randomGenerator.GenerateVector(), HandleBallUpdates, i, table);
                    this.balls.Add(newBall);
                }
                this.CollisionChecking = new Thread(() =>
                {
                    while (true)
                    {
                        CheckBallCollisions();
                    }
                });
                foreach (IBallType ball in balls)
                {
                    ball.Start();
                }
                CollisionChecking.IsBackground = true;
                CollisionChecking.Start();
            }
        }

        public override List<Vector2> GetBallPositions()
        {
            throw new NotImplementedException();
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
    }
}
