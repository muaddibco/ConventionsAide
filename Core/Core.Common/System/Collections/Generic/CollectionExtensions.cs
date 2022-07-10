using PostSharp.Patterns.Contracts;
using System.Linq;

namespace System.Collections.Generic
{
    public static class CollectionExtensions
    {
        //
        // Summary:
        //     Checks whatever given collection object is null or has no item.
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            if (source != null)
            {
                return source.Count <= 0;
            }

            return true;
        }

        //
        // Summary:
        //     Adds an item to the collection if it's not already in the collection.
        //
        // Parameters:
        //   source:
        //     The collection
        //
        //   item:
        //     Item to check and add
        //
        // Type parameters:
        //   T:
        //     Type of the items in the collection
        //
        // Returns:
        //     Returns True if added, returns False if not.
        public static bool AddIfNotContains<T>([NotNull] this ICollection<T> source, T item)
        {
            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }

        //
        // Summary:
        //     Adds items to the collection which are not already in the collection.
        //
        // Parameters:
        //   source:
        //     The collection
        //
        //   items:
        //     Item to check and add
        //
        // Type parameters:
        //   T:
        //     Type of the items in the collection
        //
        // Returns:
        //     Returns the added items.
        public static IEnumerable<T> AddIfNotContains<T>([NotNull] this ICollection<T> source, IEnumerable<T> items)
        {
            List<T> list = new();
            foreach (T item in items)
            {
                if (!source.Contains(item))
                {
                    source.Add(item);
                    list.Add(item);
                }
            }

            return list;
        }

        //
        // Summary:
        //     Adds an item to the collection if it's not already in the collection based on
        //     the given predicate.
        //
        // Parameters:
        //   source:
        //     The collection
        //
        //   predicate:
        //     The condition to decide if the item is already in the collection
        //
        //   itemFactory:
        //     A factory that returns the item
        //
        // Type parameters:
        //   T:
        //     Type of the items in the collection
        //
        // Returns:
        //     Returns True if added, returns False if not.
        public static bool AddIfNotContains<T>([NotNull] this ICollection<T> source, [NotNull] Func<T, bool> predicate, [NotNull] Func<T> itemFactory)
        {
            if (source.Any(predicate))
            {
                return false;
            }

            source.Add(itemFactory());
            return true;
        }

        //
        // Summary:
        //     Removes all items from the collection those satisfy the given predicate.
        //
        // Parameters:
        //   source:
        //     The collection
        //
        //   predicate:
        //     The condition to remove the items
        //
        // Type parameters:
        //   T:
        //     Type of the items in the collection
        //
        // Returns:
        //     List of removed items
        public static IList<T> RemoveAll<T>(this ICollection<T> source, Func<T, bool> predicate)
        {
            List<T> list = source.Where(predicate).ToList();
            foreach (T item in list)
            {
                source.Remove(item);
            }

            return list;
        }

        //
        // Summary:
        //     Removes all items from the collection.
        //
        // Parameters:
        //   source:
        //     The collection
        //
        //   items:
        //     Items to be removed from the list
        //
        // Type parameters:
        //   T:
        //     Type of the items in the collection
        public static void RemoveAll<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                source.Remove(item);
            }
        }
    }
}
