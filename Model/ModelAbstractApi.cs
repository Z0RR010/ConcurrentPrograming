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
        public abstract float TableHeight { get; }
        public abstract float TableWidth { get; }
        public abstract float BorderWidth { get; }

        public static ModelAbstractApi CreateApi(LogicAbstractApi? logicApi = null)
        {
            return new ModelApi(logicApi ?? LogicAbstractApi.CreateApi());
        }

        public abstract void GenerateBalls(int number, EventHandler update);

        public abstract void Stop();

        public abstract ObservableCollection<IVisualBall> GetVisualBalls();

        public abstract void Update(object? sender, PositionUpdateArgs eventArgs);
    }
}
