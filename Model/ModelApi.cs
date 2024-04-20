namespace Model
{
    internal class ModelApi : ModelAbstractApi
    {
        public override int TableHeight => 300;
        public override int TableWidth => 600;
        public override int BorderWidth => TableWidth + 10;
        public override int BallRadius => 10;
    }
}