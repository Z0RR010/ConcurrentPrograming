using System.ComponentModel;

namespace Model
{
    public abstract class IVisualBall
    {
        public static IVisualBall CreateVisualBall(double x, double y)
        {
            return new VisualBall(x, y);
        }

        public abstract double PositionX { get; set; }
        public abstract double PositionY { get; set; }

        public abstract void UpdateVisualBall(Object o, PropertyChangedEventArgs e);

        public abstract event PropertyChangedEventHandler? PropertyChanged;
    }
}