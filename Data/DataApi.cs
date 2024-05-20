using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Numerics;

namespace Data
{
    public class DataApi : DataAbstractApi
    {

        public override ICollection<T> GetRepository<T>()
        {
            return new Repository<T>();
        }

        public override IBallType GetBall(Vector2 Pos, Vector2 Move, EventHandler<BallPositionChange> eventHandler, int id, Table table)
        {
            return new Ball(Pos, Move, eventHandler, id, table);
        }

        public override Table GetTable()
        {
            return new Table();
        }
    }
}