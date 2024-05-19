using NUnit.Framework;
using Logic;
using System.Collections.ObjectModel;
using Timer = System.Timers.Timer;
using Data;
using System.Numerics;

namespace LogicUnitTest
{
    public class LogicTest
    {
        [TestFixture]
        public class LogicApiTests
        {
            private class MockDataApi : DataAbstractApi
            {
                private List<IBallType> balls = new List<IBallType>();

                public override ICollection<T> GetRepository<T>()
                {
                    return (ICollection<T>)balls;
                }

                public override IBallType GetBall(Vector2 position, Vector2 speed, EventHandler<BallPositionChange> eventHandler, int id, Table table)
                {
                    return new TestBall(position, speed, eventHandler, id, table);
                }
            }

            private class BallPositionUpdateHandler
            {
                public bool EventTriggered { get; private set; } = false;
                public Vector2 Position { get; private set; }
                public int ID { get; private set; }

                public void HandlePositionUpdate(object sender, PositionUpdateArgs e)
                {
                    EventTriggered = true;
                    Position = e.Position;
                    ID = e.ID;
                }
            }

            private MockDataApi dataApi;
            private BallPositionUpdateHandler positionUpdateHandler;

            [SetUp]
            public void Setup()
            {
                dataApi = new MockDataApi();
                positionUpdateHandler = new BallPositionUpdateHandler();
            }

            [Test]
            public void LogicApi_CreatesBalls()
            {
                var logicApi = LogicAbstractApi.CreateApi();
                int ballCount = 5;
                logicApi.GenerateHandler(ballCount, positionUpdateHandler.HandlePositionUpdate);

                var (positions, radii) = logicApi.GetBallInfo();
                Assert.AreEqual(ballCount, positions.Count);
                Assert.AreEqual(ballCount, radii.Count);
            }

            [Test]
            public void LogicApi_UpdatesBallPositions()
            {
                var logicApi = LogicAbstractApi.CreateApi();
                int ballCount = 1;
                logicApi.GenerateHandler(ballCount, positionUpdateHandler.HandlePositionUpdate);
                Thread.Sleep(100);
                logicApi.Stop();

                Assert.IsTrue(positionUpdateHandler.EventTriggered);
            }

            [Test]
            public void LogicApi_StopsBalls()
            {
                var logicApi = LogicAbstractApi.CreateApi();
                int ballCount = 3;
                logicApi.GenerateHandler(ballCount, positionUpdateHandler.HandlePositionUpdate);
                logicApi.Stop();

                var (positions, radii) = logicApi.GetBallInfo();
                Assert.AreEqual(0, positions.Count);
                Assert.AreEqual(0, radii.Count);
            }

        }
        
    }
}