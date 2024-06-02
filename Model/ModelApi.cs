using Logic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Windows.Input;
using Timer = System.Timers.Timer;

namespace Model
{
    internal class ModelApi : ModelAbstractApi
    {
        public override float TableHeight => 300 * Scale;
        public override float TableWidth => 600 * Scale;
        public override float BorderWidth => TableWidth + 10;
        public float Scale = 1f;

        public LogicAbstractApi LogicApi;

        public Timer Timer;

        private ObservableCollection<IVisualBall> balls;



        public override void GenerateBalls(int number)
        {
            if (balls.Count != 0)
            {
                return;
            }
            LogicApi.GenerateHandler(number);
            var lists = LogicApi.GetBallInfo();
            List<Vector2> positions = lists.Item1;
            List<int> radius = lists.Item2;
            balls.Clear();
            List<EventHandler<ReadOnlyCollection<float>>> eventHandlers = new();
            foreach (int i in Enumerable.Range(0, positions.Count))
            {
                IVisualBall ball = new VisualBall(positions[i], radius[i], Scale);
                eventHandlers.Add(ball.UpdateVisualBall);
                balls.Add(ball);
            }
            LogicApi.ConnectBalls(eventHandlers);
        }

        public override void Stop()
        {
            LogicApi.Stop();
            balls.Clear();
        }


        public override ObservableCollection<IVisualBall> GetVisualBalls()
        {
            return balls;
        }

        public ModelApi(LogicAbstractApi logicApi)
        {
            this.LogicApi = logicApi;
            this.balls = new ObservableCollection<IVisualBall>();
            
        }



    }
}