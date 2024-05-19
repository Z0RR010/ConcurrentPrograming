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

        //[SetUp]
        //public void Setup()
        //{
        //}

        //[Test]
        //public void ConstructorTest()
        //{
        //    //var ball = new Ball(1, 1, (0, 0));
        //    //Assert.IsNotNull(ball);
        //    //Assert.That(ball.X, Is.EqualTo(1));
        //}

        //[Test]
        //public void Ball_SetXValue()
        //{
        //    // Arrange
        //    //var ball = new Ball(0, 0, (0, 0));
        //    //double newX = 15;

        //    // Act
        //    //ball.X = newX;

        //    // Assert
        //    //Assert.That(ball.X, Is.EqualTo(newX));
        //}

        //[Test]
        //public void Ball_SetYValue()
        //{
        //    // Arrange
        //    //var ball = new Ball(0, 0, (0, 0));
        //    //double newY = 25;

        //    // Act
        //    //ball.Y = newY;

        //    // Assert
        //    //Assert.That(ball.Y, Is.EqualTo(newY));
        //}

        //[Test]
        //public void GenerateHandler_AddsBallsToCollection()
        //{
        //    // Arrange
        //    var logicApi = LogicAbstractApi.CreateApi(new TestDataApi());
        //    var balls = logicApi.CreateRepository();
        //    var ballsNumber = 5;
        //    EventHandler<PositionUpdateArgs> eventHandler;

        //// Act
        //    logicApi.GenerateHandler(ballsNumber, eventHandler);

        //    // Assert
        //    Assert.That(balls.Count, Is.EqualTo(ballsNumber));
        //    foreach (var ball in balls)
        //    {
        //        //Assert.That(ball.X >= minX && ball.X <= maxX && ball.Y >= minY && ball.Y <= maxY, Is.True);
        //    }
        //}        

        ////[Test]
        ////public void MoveBall_MovesBallsWithinBounds()
        ////{
        ////    // Arrange
        ////    var logicApi = LogicAbstractApi.CreateApi(new TestDataApi());
        ////    var balls = logicApi.CreateRepository();
        ////    //balls.Add(new Ball(50, 50, (1, 1)));
        ////    //balls.Add(new Ball(20, 20, (-1, -1)));

        ////    var maxX = 100;
        ////    var maxY = 100;

        ////    // Act
        ////    logicApi.MoveBalls(balls, 5, maxX, maxY);

        ////    // Assert
        ////    //Assert.That(balls.Any(ball => ball.X == 51));
        ////    //Assert.That(balls.Any(ball => ball.Y == 51));
        ////    //Assert.That(balls.Any(ball => ball.X == 19));
        ////    //Assert.That(balls.Any(ball => ball.Y == 19));
        ////}

        ////[Test]
        ////public void MoveBall_BouncesBallsAtEdges()
        ////{
        ////    // Arrange
        ////    var logicApi = LogicAbstractApi.CreateApi();
        ////    var balls = logicApi.CreateRepository();

        ////    //balls.Add(new Ball(5, 6, (-1, -1)));
        ////    //balls.Add(new Ball(95, 90, (1, 1)));

        ////    var maxX = 100;
        ////    var maxY = 100;

        ////    // Act
        ////    logicApi.MoveBalls(balls, 5, maxX, maxY);

        ////    // Assert
        ////    //Assert.That(balls.Any(ball => ball.X == 4.0));
        ////    //Assert.That(balls.Any(ball => ball.Y == 5.0));
        ////    //Assert.That(balls.Any(ball => ball.X == 95));
        ////    //Assert.That(balls.Any(ball => ball.Y == 91));
        ////}
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
                    return new Ball(position, speed, eventHandler, id, table);
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
            public void LogicApi_HandlesBallCollisions()
            {
                int ballCount = 2;
                bool collisionDetected = false;

                // Override GetBall to create balls at specific positions to ensure collision
                dataApi = new MockDataApi();
                var mockTable = new Table();
                var ball1 = new Ball(new Vector2(100, 100), new Vector2(1, 0), (sender, e) => { }, 0, mockTable);
                var ball2 = new Ball(new Vector2(110, 100), new Vector2(-1, 0), (sender, e) => { }, 1, mockTable);

                dataApi.GetRepository<IBallType>().Add(ball1);
                dataApi.GetRepository<IBallType>().Add(ball2);
                var logicApi = LogicAbstractApi.CreateApi();

                logicApi.GenerateHandler(ballCount, positionUpdateHandler.HandlePositionUpdate);
                Thread.Sleep(1000);
                logicApi.Stop();

                collisionDetected = ball1.Speed.X != 1 && ball2.Speed.X != -1;
                Assert.IsTrue(collisionDetected);
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

            [Test]
            public void Stop_StopsTimer()
            {
                // Arrange
                var logicApi = LogicAbstractApi.CreateApi();
                var timer = new Timer { Enabled = true };

                // Act
                logicApi.Stop();

                // Assert
                Assert.IsFalse(timer.Enabled);
            }
        }
        
    }
}