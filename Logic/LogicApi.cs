using System.Collections;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Timer = System.Timers.Timer;

namespace Logic;
    public class LogicApi : LogicAbstractApi
    {
        public override void GenerateHandler(ICollection<Ball> coordinates, int ballsNumber, int minX, int maxX, int minY, int maxY)
        {
            var randomGenerator = new Randomizer();
            if (coordinates.Count != 0) return;
            for (var i = 0; i < ballsNumber; i++)
            {
                var newBall = new Ball(randomGenerator.GenerateDouble(minX, maxX),
                    randomGenerator.GenerateDouble(minY, maxY),
                    randomGenerator.GenerateVector());
                coordinates.Add(newBall);
            }
        }

        public override void MovingHandler(ObservableCollection<Ball> coordinates, Timer timer, int ballsNumber, int radius,
            int maxX, int maxY)
        {
            if (ballsNumber == 0) return;

            var context = SynchronizationContext.Current;
            timer.Interval = 30;
            timer.Elapsed += (_, _) => context.Send(_ => MoveBall(coordinates, radius, maxX, maxY), null);
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public override void MoveBall(ObservableCollection<Ball> coordinates, int radius, int maxX, int maxY)
        {
            var randomGenerator = new Randomizer();
            var copy = coordinates;
            for (var i = 0; i < coordinates.Count; i++)
            {
            // Generate shifts
                var xShift = copy[i].Direction.moveX;
                var yShift = copy[i].Direction.moveY;
                var newBall = new Ball(copy[i].X + xShift, copy[i].Y + yShift, copy[i].Direction);
                // Prevent exceeding canvas
                if (newBall.X - radius < 0) newBall = new Ball(radius, newBall.Y, copy[i].Direction);
                if (newBall.X + radius > maxX) newBall = new Ball(maxX - radius, newBall.Y, copy[i].Direction);
                if (newBall.Y - radius < 0) newBall = new Ball(newBall.X, radius, copy[i].Direction);
                if (newBall.Y + radius > maxY) newBall = new Ball(newBall.X, maxY - radius, copy[i].Direction);
                copy[i] = newBall;
            }
            // Refresh collection to subscribe PropertyChange event by setter
            coordinates = new ObservableCollection<Ball>(copy);
        }

        public override void Stop(Timer timer)
        {
            timer.Enabled = false;
        }

        public override void ClearBalls(Timer timer, IList coordinates)
        {
            Stop(timer);
            coordinates.Clear();
        }

    }
}
}