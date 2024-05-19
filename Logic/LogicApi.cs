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

        private EventHandler<PositionUpdateArgs> updatehandler;


        public LogicApi(DataAbstractApi data) 
        {
            this.dataApi = data;
            this.balls = dataApi.GetRepository<IBallType>();
        }

        public override ICollection<IBallType> CreateRepository()
        {
            return this.dataApi.GetRepository<IBallType>();
        }

        public override void GenerateHandler(int ballsNumber, int minX, int maxX, int minY, int maxY, EventHandler<PositionUpdateArgs> eventHandler)
        {
            this.updatehandler = eventHandler;
            var randomGenerator = new Randomizer();
            if (this.balls.Count != 0 || ballsNumber == 0) return;
            foreach (int i in Enumerable.Range(0,ballsNumber - 1))
            {
                var newBall = this.dataApi.GetBall(new Vector2(randomGenerator.GenerateFloat(0, maxX), randomGenerator.GenerateFloat(0, maxY)), randomGenerator.GenerateVector(), HandleBallUpdates, i);
                this.balls.Add(newBall);
            }
        }

        public override void Stop(Timer timer)
        {
            timer.Enabled = false;
        }

        void HandleBallUpdates(object? ball, BallPositionChange Position)
        {
            int id = Position.ID;
            Vector2 pos = Position.Position;
            updatehandler.Invoke(this, new PositionUpdateArgs(pos, id));
        }

        public override List<Vector2> GetBallPositions()
        {
            List<Vector2> ret = new List<Vector2>();
            foreach (var ball in this.balls) 
            {
                ret.Add(ball.Position);
            }
            return ret;
        }
    }

}