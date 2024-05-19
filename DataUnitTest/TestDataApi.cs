using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace DataUnitTest
{
    internal class TestDataApi : DataAbstractApi
    {
        public TestDataApi() { }

        public override IBallType GetBall(Vector2 Pos, Vector2 Move, EventHandler<BallPositionChange> eventHandler, int id, Table table)
        {
            return new Ball(Pos, Move, eventHandler, id, table);
        }

        public override ICollection<T> GetRepository<T>()
        {
            return new Repository<T>();
        }
    }
}
