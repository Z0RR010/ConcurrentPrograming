using NUnit.Framework;
using Data;
using System.Collections.ObjectModel;
using Timer = System.Timers.Timer;
using System.Numerics;

namespace DataUnitTest
{
    public class BallTest
    {
        [TestFixture]
        public class BallTests
        {
            private class MockTable : Table
            {

            }

            private class BallPositionChangeHandler
            {
                public bool EventTriggered { get; private set; } = false;
                public Vector2 Position { get; private set; }
                public int ID { get; private set; }

                public void HandlePositionChange(object sender, BallPositionChange e)
                {
                    EventTriggered = true;
                    Position = e.Position;
                    ID = e.ID;
                }
            }

            private MockTable table;
            private BallPositionChangeHandler handler;

            [SetUp]
            public void Setup()
            {
                table = new MockTable();
                handler = new BallPositionChangeHandler();
            }

            [Test]
            public void Ball_StartsAndMoves()
            {
                var initialPosition = new Vector2(100, 100);
                var initialSpeed = new Vector2(5, 5);
                var ball = new Ball(initialPosition, initialSpeed, handler.HandlePositionChange, 1, table);

                ball.Start();
                Thread.Sleep(100);
                ball.Stop();

                Assert.IsTrue(handler.EventTriggered);
                Assert.AreNotEqual(initialPosition, ball.Position);
            }

            [Test]
            public void Ball_BouncesOffWalls()
            {
                var initialPosition = new Vector2(290, 100); // Near the right wall
                var initialSpeed = new Vector2(10, 0); // Moving right
                var ball = new Ball(initialPosition, initialSpeed, handler.HandlePositionChange, 1, table);

                ball.Start();
                Thread.Sleep(100);
                ball.Stop();

                Assert.IsTrue(handler.EventTriggered);
                Assert.LessOrEqual(ball.Position.X, table.TableWidth - ball.Radius);
                Assert.AreEqual(-10, ball.Speed.X);
            }

            [Test]
            public void Ball_CorrectlyHandlesSpeedUpdate()
            {
                var initialPosition = new Vector2(100, 100);
                var initialSpeed = new Vector2(5, 5);
                var ball = new Ball(initialPosition, initialSpeed, handler.HandlePositionChange, 1, table);

                var newSpeed = new Vector2(-5, -5);
                ball.UpdateSpeed(newSpeed);
                ball.Start();
                Thread.Sleep(100);
                ball.Stop();

                Assert.IsTrue(handler.EventTriggered);
                Assert.AreEqual(newSpeed, ball.Speed);
            }
        }
    }
}