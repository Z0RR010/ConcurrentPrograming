using Logic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    internal class VisualBall : IVisualBall, INotifyPropertyChanged
    {
        public override double PositionX { get => _PositionX; set { _PositionX = value; RaisePropertyChanged(); } }
        public override double PositionY { get => _PositionY; set { _PositionY = value; RaisePropertyChanged(); } }

        private double _PositionX { get; set; }
        private double _PositionY { get; set; }

        public VisualBall(double x, double y)
        {
            _PositionX = x;
            _PositionY = y;
        }

        public override event PropertyChangedEventHandler? PropertyChanged;

        public override void UpdateVisualBall(Object o, PropertyChangedEventArgs e)
        {
            
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
