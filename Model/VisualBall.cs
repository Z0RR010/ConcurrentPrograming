using Logic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Numerics;
using System.Collections.ObjectModel;

namespace Model
{
    internal class VisualBall : IVisualBall, INotifyPropertyChanged
    {

        public override float PositionX { get; set; }
        public override float PositionY { get; set; }
        public override float Radius{ get; set; }
        private static float Scale;

        public VisualBall(Vector2 pos, int radius, float scale) //relacja model-logic, zachodzi tu na granicy jedna ważna rzecz. Jaka?
        {
            Scale = scale;
            Radius = radius * scale;
            PositionX= pos.X * scale;
            PositionY= pos.Y * scale;
        }

        public override event PropertyChangedEventHandler? PropertyChanged;

        public override void UpdateVisualBall(object o, ReadOnlyCollection<float> pos)
        {
            this.PositionX = pos.First() * Scale;
            this.PositionY = pos.Last() * Scale;
            RaisePropertyChanged(nameof(PositionX));
            RaisePropertyChanged(nameof(PositionY));
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
