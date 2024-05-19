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
        private static float scale = 1f;

        public VisualBall(Vector2 pos)
        {
            PositionX= pos.X * scale;
            PositionY= pos.Y * scale;
        }

        public override event PropertyChangedEventHandler? PropertyChanged;

        public override void UpdateVisualBall(Vector2 pos)
        {
            this.PositionX = pos.X * scale;
            this.PositionY = pos.Y * scale;
            RaisePropertyChanged(nameof(PositionX));
            RaisePropertyChanged(nameof(PositionY));
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
