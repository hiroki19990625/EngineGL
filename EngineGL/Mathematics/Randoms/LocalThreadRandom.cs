using System;
using System.Threading;

namespace EngineGL.Mathematics.Randoms
{
    /// <summary>
    /// スレッドセーフな擬似乱数ジェネレーターを生成する機能を提供します。
    /// </summary>
    public static class LocalThreadRandom
    {
        private static int seed = Environment.TickCount;

        private static ThreadLocal<Random> _local =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        /// <summary>
        /// スレッドセーフな擬似乱数ジェネレーターを生成します。
        /// </summary>
        /// <returns></returns>
        public static Random GetRandom()
        {
            return _local.Value;
        }
    }
}