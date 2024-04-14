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

        public abstract void MovingHandler(ObservableCollection<IBallType> balls, System.Timers.Timer timer, int radius,
            int maxX, int maxY);

        public abstract void MoveBalls(ObservableCollection<IBallType> balls, int radius, int maxX, int maxY);

        public abstract void Stop(System.Timers.Timer timer);

        public abstract void ClearBalls(System.Timers.Timer timer, IList balls);

        public static LogicAbstractApi CreateApi()
        {
            return new LogicApi();
        }
    }
}
