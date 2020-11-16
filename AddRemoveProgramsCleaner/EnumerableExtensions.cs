using System.Collections.Generic;
using System.Linq;

namespace AddRemoveProgramsCleaner {

    public static class EnumerableExtensions {

        public static IEnumerable<T> Compact<T>(this IEnumerable<T?> source) where T: class {
            return source.Where(item => item != null)!;
        }

        public static IEnumerable<T> Compact<T>(this IEnumerable<T?> source) where T: struct {
            return (IEnumerable<T>) source.Where(item => item != null);
        }

    }

}