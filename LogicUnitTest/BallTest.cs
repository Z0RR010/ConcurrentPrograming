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
            var ball = new Ball(1, 1, (0, 0));
            Assert.IsNotNull(ball);
            Assert.That(ball.X, Is.EqualTo(1));
        }

        [Test]
        public void Ball_SetXValue()
        {
            // Arrange
            var ball = new Ball(0, 0, (0, 0));
            double newX = 15;

            // Act
            ball.X = newX;

            // Assert
            Assert.That(ball.X, Is.EqualTo(newX));
        }

        [Test]
        public void Ball_SetYValue()
        {
            // Arrange
            var ball = new Ball(0, 0, (0, 0));
            double newY = 25;

            // Act
            ball.Y = newY;

            // Assert
            Assert.That(ball.Y, Is.EqualTo(newY));
        }

        [Test]
        public void GenerateHandler_AddsBallsToCollection()
        {
            // Arrange
            var logicApi = new LogicApi();
            var balls = new List<IBallType>();
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
                Assert.That(ball.X >= minX && ball.X <= maxX && ball.Y >= minY && ball.Y <= maxY, Is.True);
            }
        }

        [Test]
        public void MovingHandler_StartsTimerWithBalls()
        {
            // Arrange
            var logicApi = new LogicApi();
            var balls = new ObservableCollection<IBallType> { new Ball(0, 0, (0, 0)) };
            var timer = new Timer();

            // Act
            logicApi.MovingHandler(balls, timer, 10, 100, 100);

            // Assert
            Assert.That(timer.Enabled, Is.True);
        }

        [Test]
        public void MovingHandler_StartsTimerWithoutBalls()
        {
            // Arrange
            var logicApi = new LogicApi();
            var balls = new ObservableCollection<IBallType> {};
            var timer = new Timer();

            // Act
            logicApi.MovingHandler(balls, timer, 10, 100, 100);

            // Assert
            Assert.That(timer.Enabled, Is.False);
        }

        [Test]
        public void MoveBall_MovesBallsWithinBounds()
        {
            // Arrange
            var logicApi = new LogicApi();
            var balls = new ObservableCollection<IBallType>
            {
                new Ball(50, 50, (1, 1)),
                new Ball(20, 20, (-1, -1))
            };
            var maxX = 100;
            var maxY = 100;

            // Act
            logicApi.MoveBalls(balls, 5, maxX, maxY);

            // Assert
            Assert.That(balls[0].X, Is.EqualTo(51));
            Assert.That(balls[0].Y, Is.EqualTo(51));
            Assert.That(balls[1].X, Is.EqualTo(19));
            Assert.That(balls[1].Y, Is.EqualTo(19));
        }

        [Test]
        public void MoveBall_BouncesBallsAtEdges()
        {
            // Arrange
            var logicApi = new LogicApi();
            var balls = new ObservableCollection<IBallType>
            {
                new Ball(5, 6, (-1, -1)),
                new Ball(95, 90, (1, 1))
            };
            var maxX = 100;
            var maxY = 100;

            // Act
            logicApi.MoveBalls(balls, 5, maxX, maxY);

            // Assert
            Assert.That(balls[0].X, Is.EqualTo(4.0));
            Assert.That(balls[0].Y, Is.EqualTo(5.0));
            Assert.That(balls[1].X, Is.EqualTo(95));
            Assert.That(balls[1].Y, Is.EqualTo(91));
        }

        [Test]
        public void Stop_StopsTimer()
        {
            // Arrange
            var logicApi = new LogicApi();
            var timer = new Timer { Enabled = true };

            // Act
            logicApi.Stop(timer);

            // Assert
            Assert.IsFalse(timer.Enabled);
        }

        [Test]
        public void ClearBalls_StopsTimerAndClearsCollection()
        {
            // Arrange
            var logicApi = new LogicApi();
            var timer = new Timer { Enabled = true };
            var balls = new List<Ball> { new Ball(0, 0, (0, 0)), new Ball(0, 0, (0, 0)) };

            // Act
            logicApi.ClearBalls(timer, balls);

            // Assert
            Assert.IsFalse(timer.Enabled);
            Assert.That(balls.Count, Is.EqualTo(0));
        }
    }
}