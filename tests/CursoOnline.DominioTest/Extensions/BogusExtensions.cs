using Bogus;
using System;

namespace CursoOnline.DominioTest.Extensions
{
    public static class BogusExtensions
    {
        public static int NumberPositive(this Randomizer randomizer, int minValue = 1, int maxValue = int.MaxValue)
        {
            return randomizer.Number(minValue, maxValue);
        }

        public static decimal DecimalPositive(this Randomizer randomizer, decimal minValue = 1, decimal maxValue = decimal.MaxValue)
        {
            return randomizer.Decimal(minValue, maxValue);
        }

        public static double DoublePositive(this Randomizer randomizer, double minValue = 1, double maxValue = double.MaxValue)
        {
            return randomizer.Double(minValue, maxValue);
        }

        public static decimal Round(this decimal d, int decimals = 2)
        {
            return decimal.Round(d, decimals);
        }

        public static double Round(this double d, int decimals = 2)
        {
            return Math.Round(d, decimals);
        }
    }
}
