using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public abstract void GenerateHandler(ICollection<IBallType> balls, int ballsNumber, int minX, int maxX, int minY,
        int maxY);

        public abstract ICollection<IBallType> CreateRepository();

        public abstract void MoveBalls(ICollection<IBallType> balls, int radius, int maxX, int maxY);

        public abstract void Stop(System.Timers.Timer timer);

        public static LogicAbstractApi CreateApi(DataAbstractApi? data = null)
        {
            return new LogicApi(data ?? DataAbstractApi.CreateApi());
        }
    }
}
