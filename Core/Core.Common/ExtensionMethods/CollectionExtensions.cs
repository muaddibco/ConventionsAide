using System.Collections.Generic;

namespace ConventionsAide.Core.Common.ExtensionMethods
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Determines whether the given collection is null or empty.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T> collection)
        {
            return
                collection is null ||
                collection.Count == 0;
        }
    }
}
