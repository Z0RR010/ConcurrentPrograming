using System.ComponentModel;
using System.Data.Entity;
using System.Numerics;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract ICollection<T> GetRepository<T>() where T : class;

        public static DataAbstractApi CreateApi()
        {
            return new DataApi();
        }

        public abstract IBallType GetBall(Vector2 Pos, Vector2 Move, EventHandler<BallPositionChange> eventHandler, int id, Table table);
    }
}
