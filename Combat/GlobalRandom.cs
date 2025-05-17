namespace Metamon.Combat
{
    public static class GlobalRandom
    {
        private static Random _rng = new Random();

        public static void SetSeed(int seed)
        {
            _rng = new Random(seed);
        }

        public static bool NextBool()
        {
            return _rng.Next(2) == 0;
        }

        public static int NextInt(int min, int max)
        {
            return _rng.Next(min, max);
        }

        public static float NextFloat()
        {
            return (float)_rng.NextDouble();
        }
    }
}
