using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public abstract void GenerateHandler(int ballsNumber, EventHandler<PositionUpdateArgs> eventHandler);

        public abstract ICollection<IBallType> CreateRepository();

        public abstract void Stop();

        public abstract (List<Vector2>, List<int>) GetBallInfo();

        public static LogicAbstractApi CreateApi(DataAbstractApi? data = null)
        {
            return new LogicApi(data ?? DataAbstractApi.CreateApi());
        }
    }
}
