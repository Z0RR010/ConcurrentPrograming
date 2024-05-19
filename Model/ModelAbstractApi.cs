using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Logic;

namespace Model
{
    public abstract class ModelAbstractApi
    {
        public abstract int TableHeight { get; }
        public abstract int TableWidth { get; }
        public abstract int BallRadius { get; }
        public abstract int BorderWidth { get; }

        public static ModelAbstractApi CreateApi(LogicAbstractApi? logicApi = null)
        {
            return new ModelApi(logicApi ?? LogicAbstractApi.CreateApi());
        }

        public abstract void GenerateBalls(int number, EventHandler update);

        public abstract void Stop();

        public abstract void ClearBalls();

        public abstract ObservableCollection<IVisualBall> GetVisualBalls();

        public abstract void Initialize(System.Timers.Timer timer);

        public abstract void Update(object? sender, PositionUpdateArgs eventArgs);
    }
}
