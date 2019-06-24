namespace EngineGL.Utils
{
    /// <summary>
    /// <see cref="bool"/> を容易に変換出来るメソッドを提供します。
    /// </summary>
    public static class BoolHelper
    {
        /// <summary>
        /// <see cref="bool"/> を <see cref="byte"/> に変換します。
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static byte ToByte(bool b)
        {
            return b ? (byte) 1 : (byte) 0;
        }
    }
}