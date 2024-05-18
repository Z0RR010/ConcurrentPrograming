using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Numerics;

namespace Data
{
    public class DataApi : DataAbstractApi
    {
        protected readonly DbContext Context;

        public override ICollection<T> GetRepository<T>()
        {
            return new Repository<T>();
        }

        public override IBallType GetBall(Vector2 Pos, Vector2 Move, EventHandler<BallPositionChange> eventHandler)
        {
            return new Ball(Pos, Move, eventHandler);
        }
    }
}