using System;
using System.Threading;

namespace EngineGL.Utils
{
    public static class LocalThreadRandom
    {
        private static int seed = Environment.TickCount;

        private static ThreadLocal<Random> _local =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        public static Random GetRandom()
        {
            return _local.Value;
        }
    }
}