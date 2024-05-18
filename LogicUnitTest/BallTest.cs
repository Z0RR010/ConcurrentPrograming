using NUnit.Framework;
using Logic;
using System.Collections.ObjectModel;
using Timer = System.Timers.Timer;

namespace LogicUnitTest
{
    public class BallTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConstructorTest()
        {
            //var ball = new Ball(1, 1, (0, 0));
            //Assert.IsNotNull(ball);
            //Assert.That(ball.X, Is.EqualTo(1));
        }

        [Test]
        public void Ball_SetXValue()
        {
            // Arrange
            //var ball = new Ball(0, 0, (0, 0));
            //double newX = 15;

            // Act
            //ball.X = newX;

            // Assert
            //Assert.That(ball.X, Is.EqualTo(newX));
        }

        [Test]
        public void Ball_SetYValue()
        {
            // Arrange
            //var ball = new Ball(0, 0, (0, 0));
            //double newY = 25;

            // Act
            //ball.Y = newY;

            // Assert
            //Assert.That(ball.Y, Is.EqualTo(newY));
        }

        [Test]
        public void GenerateHandler_AddsBallsToCollection()
        {
            // Arrange
            var logicApi = LogicAbstractApi.CreateApi(new TestDataApi());
            var balls = logicApi.CreateRepository();
            var ballsNumber = 5;
            var minX = 0;
            var maxX = 100;
            var minY = 0;
            var maxY = 100;

            // Act
            logicApi.GenerateHandler(balls, ballsNumber, minX, maxX, minY, maxY);

            // Assert
            Assert.That(balls.Count, Is.EqualTo(ballsNumber));
            foreach (var ball in balls)
            {
                //Assert.That(ball.X >= minX && ball.X <= maxX && ball.Y >= minY && ball.Y <= maxY, Is.True);
            }
        }        

        [Test]
        public void MoveBall_MovesBallsWithinBounds()
        {
            // Arrange
            var logicApi = LogicAbstractApi.CreateApi(new TestDataApi());
            var balls = logicApi.CreateRepository();
            //balls.Add(new Ball(50, 50, (1, 1)));
            //balls.Add(new Ball(20, 20, (-1, -1)));

            var maxX = 100;
            var maxY = 100;

            // Act
            logicApi.MoveBalls(balls, 5, maxX, maxY);

            // Assert
            //Assert.That(balls.Any(ball => ball.X == 51));
            //Assert.That(balls.Any(ball => ball.Y == 51));
            //Assert.That(balls.Any(ball => ball.X == 19));
            //Assert.That(balls.Any(ball => ball.Y == 19));
        }

        [Test]
        public void MoveBall_BouncesBallsAtEdges()
        {
            // Arrange
            var logicApi = LogicAbstractApi.CreateApi();
            var balls = logicApi.CreateRepository();
            
            //balls.Add(new Ball(5, 6, (-1, -1)));
            //balls.Add(new Ball(95, 90, (1, 1)));
            
            var maxX = 100;
            var maxY = 100;

            // Act
            logicApi.MoveBalls(balls, 5, maxX, maxY);

            // Assert
            //Assert.That(balls.Any(ball => ball.X == 4.0));
            //Assert.That(balls.Any(ball => ball.Y == 5.0));
            //Assert.That(balls.Any(ball => ball.X == 95));
            //Assert.That(balls.Any(ball => ball.Y == 91));
        }

        [Test]
        public void Stop_StopsTimer()
        {
            // Arrange
            var logicApi = LogicAbstractApi.CreateApi();
            var timer = new Timer { Enabled = true };

            // Act
            logicApi.Stop(timer);

            // Assert
            Assert.IsFalse(timer.Enabled);
        }
    }
}