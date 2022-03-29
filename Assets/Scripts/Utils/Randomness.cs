using System.Collections.Generic;
using System.Linq;
using Random = System.Random;
using Color = UnityEngine.Color;

namespace Utils
{
    public static class Randomness
    {
        private static Random rand = new Random();

        public static void SetSeed(int seed) => rand = new Random(seed);

        public static T RandomElement<T>(this T[] array)
        {
            var e = rand.Next(array.Length);
            return array[e];
        }
        
        public static T[] RandomElements<T>(this IEnumerable<T> array, int n)
        {
            return array.OrderBy(x => rand.Next()).Take(n).ToArray();
        }

        public static Color RandomBlackOrWhiteColor() => rand.NextDouble() > 0.5 ? Color.white : Color.black;
        
        public static Color RandomColor()
        {
            const int max = byte.MaxValue + 1; // 256
            var r = rand.Next(max);
            var g = rand.Next(max);
            var b = rand.Next(max);
            return new Color(r, g, b);
        }
    }
}
