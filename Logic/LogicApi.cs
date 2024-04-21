using System.Collections;
using System.Collections.ObjectModel;
using Timer = System.Timers.Timer;
using Data;
using System.Runtime.CompilerServices;

namespace Logic
{ 

    internal class LogicApi : LogicAbstractApi
    {
        DataAbstractApi dataApi;


        public LogicApi(DataAbstractApi data) 
        {
            this.dataApi = data;
        }

        public override ICollection<IBallType> CreateRepository()
        {
            return this.dataApi.GetRepository<IBallType>();
        }

        public override void GenerateHandler(ICollection<IBallType> balls, int ballsNumber, int minX, int maxX, int minY, int maxY)
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


        public override void MoveBalls(ICollection<IBallType> balls, int radius, int maxX, int maxY)
        {
            foreach (var ball in balls)
            {
                ball.Move(radius, maxX, maxY);
            }
        }

        public override void Stop(Timer timer)
        {
            timer.Enabled = false;
        }

        public override void ClearBalls(Timer timer, ICollection<IBallType> balls)
        {
            Stop(timer);
            balls.Clear();
        }

    }

}