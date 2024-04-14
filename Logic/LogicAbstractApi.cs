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
        public abstract void GenerateHandler(ICollection<Ball> coordinates, int ballsNumber, int minX, int maxX, int minY,
        int maxY);

        public abstract void MovingHandler(ObservableCollection<Ball> coordinates, System.Timers.Timer timer, int radius,
            int maxX, int maxY);

        public abstract void MoveBall(ObservableCollection<Ball> coordinates, int radius, int maxX, int maxY);

        public abstract void Stop(System.Timers.Timer timer);

        public abstract void ClearBalls(System.Timers.Timer timer, IList coordinates);

        public static LogicAbstractApi CreateApi()
        {
            return new LogicApi();
        }
    }
}
