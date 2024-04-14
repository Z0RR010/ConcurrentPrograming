using NUnit.Framework;
using Logic;

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
    }
}