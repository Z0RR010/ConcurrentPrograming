using System.ComponentModel;
using System.Numerics;

namespace Model
{
    public abstract class IVisualBall
    {
        public static IVisualBall CreateVisualBall(Vector2 pos)
        {
            return new VisualBall(pos);
        }

        public abstract float PositionX { get; set; } 
        public abstract float PositionY { get; set; }
        public abstract void UpdateVisualBall(Vector2 pos);

        public abstract event PropertyChangedEventHandler? PropertyChanged;
    }
}