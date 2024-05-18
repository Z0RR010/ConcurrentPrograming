using System.Numerics;

namespace Logic
{
    public class Randomizer
    {
        private readonly Random _random;

        public Randomizer()
        {
            _random = new Random();
        }

        public float GenerateFloat(double min, double max)
        {
            return (float)((float) _random.NextDouble() * (max - min) + min);
        }

        public Vector2 GenerateVector()
        {
            // Losujemy liczby zmiennoprzecinkowe z zakresu [-1, 1] dla shiftX i shiftY
            float shiftX = (float) _random.NextDouble() * 2 - 1; // Zwraca wartość z zakresu [-1, 1]
            float shiftY = (float) _random.NextDouble() * 2 - 1; // Zwraca wartość z zakresu [-1, 1]
            return new Vector2(shiftX,shiftY);
        }

    }
}
