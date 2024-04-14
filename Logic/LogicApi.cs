using System.Collections;
using System.Collections.ObjectModel;
using Timer = System.Timers.Timer;

namespace Logic
{ 

    public class LogicApi : LogicAbstractApi
    {
        public override void GenerateHandler(ICollection<Ball> balls, int ballsNumber, int minX, int maxX, int minY, int maxY)
        {
            var randomGenerator = new Randomizer();
            if (balls.Count != 0 || ballsNumber == 0) return;
            foreach (var i in Enumerable.Range(1,ballsNumber))
            {
                var newBall = new Ball(randomGenerator.GenerateDouble(minX, maxX),
                    randomGenerator.GenerateDouble(minY, maxY),
                    randomGenerator.GenerateVector());
                balls.Add(newBall);
            }
        }

        public override void MovingHandler(ObservableCollection<Ball> balls, Timer timer, int radius,
            int maxX, int maxY)
        {
            if (balls.Count == 0) return;

            var context = SynchronizationContext.Current;
            timer.Interval = 30;
            timer.Elapsed += (_, _) => context.Send(_ => MoveBall(balls, radius, maxX, maxY), null);
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public override void MoveBall(ObservableCollection<Ball> balls, int radius, int maxX, int maxY)
        {
            var copy = balls;
            for (var i = 0; i < balls.Count; i++)
            {
            // Get shifts
                var xShift = copy[i].Direction.moveX;
                var yShift = copy[i].Direction.moveY;
                var newBall = new Ball(copy[i].X + xShift, copy[i].Y + yShift, copy[i].Direction);
                // Bounce the balls
                if (newBall.X < 0) newBall = new Ball(0, newBall.Y, (-copy[i].Direction.moveX, copy[i].Direction.moveY));
                if (newBall.X + radius > maxX) newBall = new Ball(maxX - radius, newBall.Y, (-copy[i].Direction.moveX, copy[i].Direction.moveY));
                if (newBall.Y < 0) newBall = new Ball(newBall.X, 0, (copy[i].Direction.moveX, -copy[i].Direction.moveY));
                if (newBall.Y + radius > maxY) newBall = new Ball(newBall.X, maxY - radius, (copy[i].Direction.moveX, -copy[i].Direction.moveY));
                copy[i] = newBall;
            }
            // Refresh collection to subscribe PropertyChange event by setter
            balls = new ObservableCollection<Ball>(copy);
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