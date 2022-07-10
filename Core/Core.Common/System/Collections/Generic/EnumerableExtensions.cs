﻿using System.Linq;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {
        //
        // Summary:
        //     Concatenates the members of a constructed System.Collections.Generic.IEnumerable`1
        //     collection of type System.String, using the specified separator between each
        //     member. This is a shortcut for string.Join(...)
        //
        // Parameters:
        //   source:
        //     A collection that contains the strings to concatenate.
        //
        //   separator:
        //     The string to use as a separator. separator is included in the returned string
        //     only if values has more than one element.
        //
        // Returns:
        //     A string that consists of the members of values delimited by the separator string.
        //     If values has no members, the method returns System.String.Empty.
        public static string JoinAsString(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }

        //
        // Summary:
        //     Concatenates the members of a collection, using the specified separator between
        //     each member. This is a shortcut for string.Join(...)
        //
        // Parameters:
        //   source:
        //     A collection that contains the objects to concatenate.
        //
        //   separator:
        //     The string to use as a separator. separator is included in the returned string
        //     only if values has more than one element.
        //
        // Type parameters:
        //   T:
        //     The type of the members of values.
        //
        // Returns:
        //     A string that consists of the members of values delimited by the separator string.
        //     If values has no members, the method returns System.String.Empty.
        public static string JoinAsString<T>(this IEnumerable<T> source, string separator)
        {
            return string.Join(separator, source);
        }

        //
        // Summary:
        //     Filters a System.Collections.Generic.IEnumerable`1 by given predicate if given
        //     condition is true.
        //
        // Parameters:
        //   source:
        //     Enumerable to apply filtering
        //
        //   condition:
        //     A boolean value
        //
        //   predicate:
        //     Predicate to filter the enumerable
        //
        // Returns:
        //     Filtered or not filtered enumerable based on condition
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            if (!condition)
            {
                return source;
            }

            return source.Where(predicate);
        }

        //
        // Summary:
        //     Filters a System.Collections.Generic.IEnumerable`1 by given predicate if given
        //     condition is true.
        //
        // Parameters:
        //   source:
        //     Enumerable to apply filtering
        //
        //   condition:
        //     A boolean value
        //
        //   predicate:
        //     Predicate to filter the enumerable
        //
        // Returns:
        //     Filtered or not filtered enumerable based on condition
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
        {
            if (!condition)
            {
                return source;
            }

            return source.Where(predicate);
        }
    }
}
