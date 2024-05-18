using System.Collections;
using System.Collections.ObjectModel;
using Timer = System.Timers.Timer;
using Data;
using System.Runtime.CompilerServices;
using System.Numerics;
using System.ComponentModel;
using System.Diagnostics;

namespace Logic
{ 

    internal class LogicApi : LogicAbstractApi
    {
        DataAbstractApi dataApi;

        private ICollection<IBallType> balls;


        public LogicApi(DataAbstractApi data) 
        {
            this.dataApi = data;
            this.balls = dataApi.GetRepository<IBallType>();
        }

        public override ICollection<IBallType> CreateRepository()
        {
            return this.dataApi.GetRepository<IBallType>();
        }

        public override void GenerateHandler(int ballsNumber, int minX, int maxX, int minY, int maxY)
        {
            
            var randomGenerator = new Randomizer();
            if (this.balls.Count != 0 || ballsNumber == 0) return;
            foreach (var i in Enumerable.Range(1,ballsNumber))
            {
                var newBall = this.dataApi.GetBall(new Vector2(randomGenerator.GenerateFloat(0, maxX), randomGenerator.GenerateFloat(0, maxY)), randomGenerator.GenerateVector(), handleBallUpdates);
                this.balls.Add(newBall);
            }
        }

        public override void Stop(Timer timer)
        {
            timer.Enabled = false;
        }

        void handleBallUpdates(object? ball, BallPositionChange Position)
        {
            Debug.Print(Position.Position.ToString());
        }
    }

}