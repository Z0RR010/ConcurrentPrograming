using NUnit.Framework;
using Logic;
using System.Collections.ObjectModel;
using Data;
using System.Numerics;
using System.Threading;
using System.Collections.Generic;

namespace LogicUnitTest
{
    public class LogicTest
    {
        [TestFixture]
        public class LogicApiTests
        {
            private class MockDataApi : DataAbstractApi
            {
                private List<IBallType> TestBalls = new List<IBallType>();

                public override ICollection<T> GetRepository<T>()
                {
                    return (ICollection<T>)TestBalls;
                }

                public override IBallType GetBall(Vector2 position, Vector2 speed, Table table)
                {
                    var ball = new TestBall(position, speed, table);
                    TestBalls.Add(ball);
                    return ball;
                }

                public override Table GetTable()
                {
                    return new Table();
                }
            }

            private class TestBallPositionUpdateHandler
            {
                public bool EventTriggered { get; private set; } = false;
                public Vector2 Position { get; private set; }

                public void HandlePositionUpdate(object sender, ReadOnlyCollection<float> e)
                {
                    EventTriggered = true;
                    Position = new Vector2(e[0], e[1]);
                }
            }

            private MockDataApi dataApi;
            private TestBallPositionUpdateHandler positionUpdateHandler;

            [SetUp]
            public void Setup()
            {
                dataApi = new MockDataApi();
                positionUpdateHandler = new TestBallPositionUpdateHandler();
            }

            //[Test]
            //public void LogicApi_CreatesTestBalls()
            //{
            //    var logicApi = LogicAbstractApi.CreateApi(dataApi);
            //    int TestBallCount = 5;
            //    logicApi.GenerateHandler(TestBallCount);

            //    var (positions, radii) = logicApi.GetBallInfo();
            //    Assert.AreEqual(TestBallCount, positions.Count);
            //    Assert.AreEqual(TestBallCount, radii.Count);
            //}

            //[Test]
            //public void LogicApi_UpdatesTestBallPositions()
            //{
            //    var logicApi = LogicAbstractApi.CreateApi(dataApi);
            //    int TestBallCount = 1;
            //    logicApi.GenerateHandler(TestBallCount);

            //    // Connect the event handler
            //    var balls = dataApi.GetRepository<IBallType>();
            //    foreach (var ball in balls)
            //    {
            //        ball.Connect(positionUpdateHandler.HandlePositionUpdate);
            //    }

            //    Thread.Sleep(100);
            //    logicApi.Stop();

            //    Assert.IsTrue(positionUpdateHandler.EventTriggered);
            //}

            //[Test]
            //public void LogicApi_StopsTestBalls()
            //{
            //    var logicApi = LogicAbstractApi.CreateApi(dataApi);
            //    int TestBallCount = 3;
            //    logicApi.GenerateHandler(TestBallCount);
            //    logicApi.Stop();

            //    var (positions, radii) = logicApi.GetBallInfo();
            //    Assert.AreEqual(0, positions.Count);
            //    Assert.AreEqual(0, radii.Count);
            //}


                [Test]
                public void LogicApi_CreatesBalls()
                {
                    var logicApi = LogicAbstractApi.CreateApi(dataApi);
                    int ballCount = 5;
                    logicApi.GenerateHandler(ballCount);

                    var (positions, radii) = logicApi.GetBallInfo();
                    Assert.AreEqual(ballCount, positions.Count);
                    Assert.AreEqual(ballCount, radii.Count);
                }

                [Test]
                public void LogicApi_UpdatesBallPositions()
                {
                    var logicApi = LogicAbstractApi.CreateApi(dataApi);
                    int ballCount = 1;
                    logicApi.GenerateHandler(ballCount);
                    Thread.Sleep(100);
                    logicApi.Stop();

                    Assert.IsTrue(positionUpdateHandler.EventTriggered);
                }

                [Test]
                public void LogicApi_StopsBalls()
                {
                    var logicApi = LogicAbstractApi.CreateApi(dataApi);
                    int ballCount = 3;
                    logicApi.GenerateHandler(ballCount);
                    logicApi.Stop();

                    var (positions, radii) = logicApi.GetBallInfo();
                    Assert.AreEqual(0, positions.Count);
                    Assert.AreEqual(0, radii.Count);
                }
        }
    }
}
