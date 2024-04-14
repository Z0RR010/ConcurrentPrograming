using NUnit.Framework;
using Logic;
using System.Collections.ObjectModel;
using System.Timers;
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
            Assert.AreEqual(1, ball.X);
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
            Assert.AreEqual(newX, ball.X);
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
            Assert.AreEqual(newY, ball.Y);
        }

        [Test]
        public void GenerateHandler_AddsBallsToCollection()
        {
            // Arrange
            var logicApi = new LogicApi();
            var balls = new List<Ball>();
            var ballsNumber = 5;
            var minX = 0;
            var maxX = 100;
            var minY = 0;
            var maxY = 100;

            // Act
            logicApi.GenerateHandler(balls, ballsNumber, minX, maxX, minY, maxY);

            // Assert
            Assert.AreEqual(ballsNumber, balls.Count);
            foreach (var ball in balls)
            {
                Assert.IsTrue(ball.X >= minX && ball.X <= maxX && ball.Y >= minY && ball.Y <= maxY);
            }
        }

        [Test]
        public void MovingHandler_StartsTimerWithBalls()
        {
            // Arrange
            var logicApi = new LogicApi();
            var balls = new ObservableCollection<Ball> { new Ball(0, 0, (0, 0)) };
            var timer = new Timer();

            // Act
            logicApi.MovingHandler(balls, timer, 10, 100, 100);

            // Assert
            Assert.IsTrue(timer.Enabled);
        }

        [Test]
        public void MovingHandler_StartsTimerWithoutBalls()
        {
            // Arrange
            var logicApi = new LogicApi();
            var balls = new ObservableCollection<Ball> {};
            var timer = new Timer();

            // Act
            logicApi.MovingHandler(balls, timer, 10, 100, 100);

            // Assert
            Assert.IsFalse(timer.Enabled);
        }

        [Test]
        public void MoveBall_MovesBallsWithinBounds()
        {
            // Arrange
            var logicApi = new LogicApi();
            var balls = new ObservableCollection<Ball>
            {
                new Ball(50, 50, (1, 1)),
                new Ball(20, 20, (-1, -1))
            };
            var maxX = 100;
            var maxY = 100;

            // Act
            logicApi.MoveBall(balls, 5, maxX, maxY);

            // Assert
            Assert.AreEqual(51, balls[0].X);
            Assert.AreEqual(51, balls[0].Y);
            Assert.AreEqual(19, balls[1].X);
            Assert.AreEqual(19, balls[1].Y);
        }

        [Test]
        public void MoveBall_BouncesBallsAtEdges()
        {
            // Arrange
            var logicApi = new LogicApi();
            var balls = new ObservableCollection<Ball>
            {
                new Ball(5, 6, (-1, -1)),
                new Ball(95, 90, (1, 1))
            };
            var maxX = 100;
            var maxY = 100;

            // Act
            logicApi.MoveBall(balls, 5, maxX, maxY);

            // Assert
            Assert.AreEqual(4.0, balls[0].X);
            Assert.AreEqual(5.0, balls[0].Y);
            Assert.AreEqual(95, balls[1].X);
            Assert.AreEqual(91, balls[1].Y);
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
            Assert.AreEqual(0, balls.Count);
        }
    }
}