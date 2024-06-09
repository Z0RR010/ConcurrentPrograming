using NUnit.Framework;
using Logic;
using System.Collections.ObjectModel;
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
                private List<IBallType> TestBalls = new List<IBallType>();

                public override ICollection<T> GetRepository<T>()
                {
                    return new ReadOnlyCollection<T>((IList<T>)TestBalls);
                }

                public override IBallType GetBall(Vector2 position, Vector2 speed, Table table)
                {
                    return new TestBall(position, speed, table);
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
                //public int ID { get; private set; }

                public void HandlePositionUpdate(object sender,TestBall e)
                {
                    EventTriggered = true;
                    Position = e.Position;
                    //ID = e.ID;
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

            [Test]
            public void LogicApi_CreatesTestBalls()
            {
                var logicApi = LogicAbstractApi.CreateApi(dataApi);
                int TestBallCount = 5;
                logicApi.GenerateHandler(TestBallCount);

                var (positions, radii) = logicApi.GetBallInfo();
                Assert.AreEqual(TestBallCount, positions.Count);
                Assert.AreEqual(TestBallCount, radii.Count);
            }

            [Test]
            public void LogicApi_UpdatesTestBallPositions()
            {
                var logicApi = LogicAbstractApi.CreateApi(dataApi);
                int TestBallCount = 1;
                logicApi.GenerateHandler(TestBallCount);
                Thread.Sleep(100);
                logicApi.Stop();

                Assert.IsTrue(positionUpdateHandler.EventTriggered);
            }

            [Test]
            public void LogicApi_StopsTestBalls()
            {
                var logicApi = LogicAbstractApi.CreateApi(dataApi);
                int TestBallCount = 3;
                logicApi.GenerateHandler(TestBallCount);
                logicApi.Stop();

                var (positions, radii) = logicApi.GetBallInfo();
                Assert.AreEqual(0, positions.Count);
                Assert.AreEqual(0, radii.Count);
            }
        }
    }
}