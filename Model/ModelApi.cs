using Logic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        private ICollection<IVisualBall> balls;


        public override void GenerateBalls(int number, int minX, int maxX, int minY, int maxY, ICommand command)
        {
            LogicApi.GenerateHandler(number, minX, maxX, minY, maxY);
            command.Execute(null);
        }

        public override void Stop()
        {
            this.Timer.Stop();
        }

        public override void ClearBalls()
        {
            this.balls.Clear();
        }

        public override ObservableCollection<IVisualBall> GetVisualBalls()
        {
            var repo = new ObservableCollection<IVisualBall>();
            //foreach (var ball in this.balls)
            //{
            //    IVisualBall b = IVisualBall.CreateVisualBall(5, 5);
            //    repo.Add(b);
                
            //}
            return repo;
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
            
            
            //this.balls = this.LogicApi.CreateRepository();
            
        }

        
    }
}