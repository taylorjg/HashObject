using System;
using System.Diagnostics;

namespace HashGraph
{
    internal static class FuncExtensions
    {
        public static Tuple<T, long> TimeIt<T>(this Func<T> func)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = func();
            stopwatch.Stop();
            return Tuple.Create(result, stopwatch.ElapsedTicks);
        }
    }
}
