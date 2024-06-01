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

        private event EventHandler ballsChanged;


        public override void GenerateBalls(int number, EventHandler update)
        {
            ballsChanged = update;
            foreach (int i in Enumerable.Range(number))
            {
                balls.Add(new VisualBall())
            }
            LogicApi.GenerateHandler(number, Update);
            var lists = LogicApi.GetBallInfo();
            List<Vector2> positions = lists.Item1;
            List<int> radius = lists.Item2;
            balls.Clear();
            foreach (int i in Enumerable.Range(0,positions.Count()))
            {
                balls.Add(new VisualBall(positions[i], radius[i],Scale));
            }
            ballsChanged.Invoke(this, EventArgs.Empty);
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

        public override void Update(object? sender,PositionUpdateArgs args)
        {
            int id = args.ID;
            if (id < balls.Count)
            {
                balls[id].UpdateVisualBall(args.Position);
                ballsChanged.Invoke(this, EventArgs.Empty);
            }
        }


    }
}