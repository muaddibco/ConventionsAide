using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace System
{
    public static class ObjectExtensions
    {
        //
        // Summary:
        //     Used to simplify and beautify casting an object to a type.
        //
        // Parameters:
        //   obj:
        //     Object to cast
        //
        // Type parameters:
        //   T:
        //     Type to be casted
        //
        // Returns:
        //     Casted object
        public static T As<T>(this object obj) where T : class
        {
            return (T)obj;
        }

        //
        // Summary:
        //     Converts given object to a value type using System.Convert.ChangeType(System.Object,System.Type)
        //     method.
        //
        // Parameters:
        //   obj:
        //     Object to be converted
        //
        // Type parameters:
        //   T:
        //     Type of the target object
        //
        // Returns:
        //     Converted object
        public static T To<T>(this object obj) where T : struct
        {
            if (typeof(T) == typeof(Guid))
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(obj.ToString());
            }

            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }

        //
        // Summary:
        //     Check if an item is in a list.
        //
        // Parameters:
        //   item:
        //     Item to check
        //
        //   list:
        //     List of items
        //
        // Type parameters:
        //   T:
        //     Type of the items
        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }

        //
        // Summary:
        //     Check if an item is in the given enumerable.
        //
        // Parameters:
        //   item:
        //     Item to check
        //
        //   items:
        //     Items
        //
        // Type parameters:
        //   T:
        //     Type of the items
        public static bool IsIn<T>(this T item, IEnumerable<T> items)
        {
            return items.Contains(item);
        }

        //
        // Summary:
        //     Can be used to conditionally perform a function on an object and return the modified
        //     or the original object. It is useful for chained calls.
        //
        // Parameters:
        //   obj:
        //     An object
        //
        //   condition:
        //     A condition
        //
        //   func:
        //     A function that is executed only if the condition is
        //     true
        //
        // Type parameters:
        //   T:
        //     Type of the object
        //
        // Returns:
        //     Returns the modified object (by the func if the condition is
        //     true
        //     ) or the original object if the condition is
        //     false
        public static T If<T>(this T obj, bool condition, Func<T, T> func)
        {
            if (condition)
            {
                return func(obj);
            }

            return obj;
        }

        //
        // Summary:
        //     Can be used to conditionally perform an action on an object and return the original
        //     object. It is useful for chained calls on the object.
        //
        // Parameters:
        //   obj:
        //     An object
        //
        //   condition:
        //     A condition
        //
        //   action:
        //     An action that is executed only if the condition is
        //     true
        //
        // Type parameters:
        //   T:
        //     Type of the object
        //
        // Returns:
        //     Returns the original object.
        public static T If<T>(this T obj, bool condition, Action<T> action)
        {
            if (condition)
            {
                action(obj);
            }

            return obj;
        }
    }
}
