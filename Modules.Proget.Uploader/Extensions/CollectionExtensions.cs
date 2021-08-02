using System.Collections.Generic;
using System.Linq;

namespace Modules.Proget.Uploader.Extensions
{
    public static class CollectionExtensions
    {
        public static bool HasData<T>(this IEnumerable<T> data)
            => data is not null && data.Any();

        public static IEnumerable<(int idx, T value)> WithIndex<T>(this IEnumerable<T> data, int delta = 0)
          => (data.HasData() ? data : Enumerable.Empty<T>()).Select((value, idx) => (idx: idx + delta, value));
    }
}
