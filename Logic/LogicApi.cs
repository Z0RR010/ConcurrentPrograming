using System.Collections;
using System.Collections.ObjectModel;
using Timer = System.Timers.Timer;

namespace Logic
{ 

    public class LogicApi : LogicAbstractApi
    {
        public override void GenerateHandler(ICollection<BallType> balls, int ballsNumber, int minX, int maxX, int minY, int maxY)
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

        public override void MovingHandler(ObservableCollection<BallType> balls, Timer timer, int radius,
            int maxX, int maxY)
        {
            if (balls.Count == 0) return;

            var context = SynchronizationContext.Current;
            timer.Interval = 30;
            timer.Elapsed += (_, _) => context.Send(_ => MoveBalls(balls, radius, maxX, maxY), null);
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public override void MoveBalls(ObservableCollection<BallType> balls, int radius, int maxX, int maxY)
        {
            for (var i = 0; i < balls.Count; i++)
            {
                balls[i] = balls[i].Move(radius,maxX,maxY);
            }
        }

        public override void Stop(Timer timer)
        {
            timer.Enabled = false;
        }

        public override void ClearBalls(Timer timer, IList balls)
        {
            Stop(timer);
            balls.Clear();
        }

    }

}