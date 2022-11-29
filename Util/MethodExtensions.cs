using System;
using System.Collections.Concurrent;

namespace Advent.Util {
    public static class MethodExtensions {
        /// <summary>
        /// Memoize a function call, as per https://www.aleksandar.io/post/memoization/
        /// </summary>
        public static Func<T, TResult> Memoize<T, TResult>(this Func<T, TResult> f) {
            var cache = new ConcurrentDictionary<T, TResult>();
            return a => cache.GetOrAdd(a, f);
        }
    }
}
