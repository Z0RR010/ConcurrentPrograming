using Logic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Numerics;

namespace Model
{
    internal class VisualBall : IVisualBall, INotifyPropertyChanged
    {

        public override float PositionX { get; set; }
        public override float PositionY { get; set; }

        public VisualBall(Vector2 pos)
        {
            PositionX= pos.X;
            PositionY= pos.Y;
        }

        public override event PropertyChangedEventHandler? PropertyChanged;

        public override void UpdateVisualBall(Vector2 pos)
        {
            this.PositionX = pos.X;
            this.PositionY = pos.Y;
            RaisePropertyChanged(nameof(PositionX));
            RaisePropertyChanged(nameof(PositionY));
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
