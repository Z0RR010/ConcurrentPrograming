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
        public override int TableHeight => 300;
        public override int TableWidth => 600;
        public override int BorderWidth => TableWidth + 10;
        public override int BallRadius => 10;


        public LogicAbstractApi LogicApi;

        public Timer Timer;

        private ObservableCollection<IVisualBall> balls;

        private event EventHandler ballsChanged;


        public override void GenerateBalls(int number, EventHandler update)
        {
            ballsChanged = update;
            LogicApi.GenerateHandler(number, Update);
            List<Vector2> positions = LogicApi.GetBallPositions();
            balls.Clear();
            foreach (Vector2 position in positions)
            {
                balls.Add(new VisualBall(position));
            }
            ballsChanged.Invoke(this, EventArgs.Empty);
        }

        public override void Stop()
        {
            LogicApi.Stop();
            this.balls.Clear();
        }


        public override ObservableCollection<IVisualBall> GetVisualBalls()
        {
            return balls;
        }

        public override void Initialize(Timer timer)
        {
            this.Timer= timer;
            this.Timer.Interval = 30;
            var context = SynchronizationContext.Current;
            this.Timer.AutoReset = true;
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