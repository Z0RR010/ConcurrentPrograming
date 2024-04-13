namespace Logic
{
    public class Randomizer
    {
        private readonly Random _random;

        public Randomizer()
        {
            _random = new Random();
        }

        public double GenerateDouble(double min, double max)
        {
            return _random.NextDouble() * (max - min) + min;
        }

        public (double shiftX, double shiftY) GenerateVector()
        {
            // Losujemy liczby zmiennoprzecinkowe z zakresu [-1, 1] dla shiftX i shiftY
            double shiftX = _random.NextDouble() * 2 - 1; // Zwraca wartość z zakresu [-1, 1]
            double shiftY = _random.NextDouble() * 2 - 1; // Zwraca wartość z zakresu [-1, 1]
            return (shiftX, shiftY);
        }

    }
}
