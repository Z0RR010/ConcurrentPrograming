using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;

namespace Model
{
    public abstract class IVisualBall
    {

        public abstract float PositionX { get; set; } 
        public abstract float PositionY { get; set; }
        public abstract float Radius { get; set; }
        public abstract void UpdateVisualBall(object o, ReadOnlyCollection<float> pos);

        public abstract event PropertyChangedEventHandler? PropertyChanged;
    }
}