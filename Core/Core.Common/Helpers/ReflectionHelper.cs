using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConventionsAide.Core.Common.Helpers
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// Finds types loaded into the current <see cref="AppDomain"/> that match the given predicate.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<Type> FindTypes(Func<Type, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies();

            foreach (var assembly in assemblies)
            {
                if (assembly.IsDynamic)
                {
                    continue;
                }

                Type[] exportedTypes = null;

                try
                {
                    exportedTypes = assembly.GetExportedTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    exportedTypes = ex.Types;
                }
                catch (TypeLoadException)
                {
                    // ignore
                }

                if (exportedTypes is null)
                {
                    continue;
                }

                foreach (var type in exportedTypes)
                {
                    var isMatch = predicate.Invoke(type);

                    if (isMatch)
                    {
                        yield return type;
                    }
                }
            }
        }
        //
        // Summary:
        //     Checks whether givenType implements/inherits genericType.
        //
        // Parameters:
        //   givenType:
        //     Type to check
        //
        //   genericType:
        //     Generic type
        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            TypeInfo typeInfo = givenType.GetTypeInfo();
            if (typeInfo.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            Type[] interfaces = typeInfo.GetInterfaces();
            foreach (Type type in interfaces)
            {
                if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (typeInfo.BaseType == null)
            {
                return false;
            }

            return IsAssignableToGenericType(typeInfo.BaseType, genericType);
        }

        public static List<Type> GetImplementedGenericTypes(Type givenType, Type genericType)
        {
            List<Type> result = new List<Type>();
            AddImplementedGenericTypes(result, givenType, genericType);
            return result;
        }

        private static void AddImplementedGenericTypes(List<Type> result, Type givenType, Type genericType)
        {
            TypeInfo typeInfo = givenType.GetTypeInfo();
            if (typeInfo.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                result.AddIfNotContains(givenType);
            }

            Type[] interfaces = typeInfo.GetInterfaces();
            foreach (Type type in interfaces)
            {
                if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType)
                {
                    result.AddIfNotContains(type);
                }
            }

            if (!(typeInfo.BaseType == null))
            {
                AddImplementedGenericTypes(result, typeInfo.BaseType, genericType);
            }
        }

        //
        // Summary:
        //     Tries to gets an of attribute defined for a class member and it's declaring type
        //     including inherited attributes. Returns default value if it's not declared at
        //     all.
        //
        // Parameters:
        //   memberInfo:
        //     MemberInfo
        //
        //   defaultValue:
        //     Default value (null as default)
        //
        //   inherit:
        //     Inherit attribute from base classes
        //
        // Type parameters:
        //   TAttribute:
        //     Type of the attribute
        public static TAttribute GetSingleAttributeOrDefault<TAttribute>(MemberInfo memberInfo, TAttribute defaultValue = null, bool inherit = true) where TAttribute : Attribute
        {
            if (memberInfo.IsDefined(typeof(TAttribute), inherit))
            {
                return memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>().First();
            }

            return defaultValue;
        }

        //
        // Summary:
        //     Tries to gets an of attribute defined for a class member and it's declaring type
        //     including inherited attributes. Returns default value if it's not declared at
        //     all.
        //
        // Parameters:
        //   memberInfo:
        //     MemberInfo
        //
        //   defaultValue:
        //     Default value (null as default)
        //
        //   inherit:
        //     Inherit attribute from base classes
        //
        // Type parameters:
        //   TAttribute:
        //     Type of the attribute
        public static TAttribute GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<TAttribute>(MemberInfo memberInfo, TAttribute defaultValue = null, bool inherit = true) where TAttribute : class
        {
            object obj = memberInfo.GetCustomAttributes(inherit: true).OfType<TAttribute>().FirstOrDefault();
            if (obj == null)
            {
                Type? declaringType = memberInfo.DeclaringType;
                obj = (((object)declaringType != null) ? declaringType.GetTypeInfo().GetCustomAttributes(inherit: true).OfType<TAttribute>()
                    .FirstOrDefault() : null) ?? defaultValue;
            }

            return (TAttribute)obj;
        }

        //
        // Summary:
        //     Tries to gets attributes defined for a class member and it's declaring type including
        //     inherited attributes.
        //
        // Parameters:
        //   memberInfo:
        //     MemberInfo
        //
        //   inherit:
        //     Inherit attribute from base classes
        //
        // Type parameters:
        //   TAttribute:
        //     Type of the attribute
        public static IEnumerable<TAttribute> GetAttributesOfMemberOrDeclaringType<TAttribute>(MemberInfo memberInfo, bool inherit = true) where TAttribute : class
        {
            IEnumerable<TAttribute> enumerable = memberInfo.GetCustomAttributes(inherit: true).OfType<TAttribute>();
            IEnumerable<TAttribute> enumerable2 = memberInfo.DeclaringType?.GetTypeInfo().GetCustomAttributes(inherit: true).OfType<TAttribute>();
            if (enumerable2 == null)
            {
                return enumerable;
            }

            return enumerable.Concat(enumerable2).Distinct();
        }

        //
        // Summary:
        //     Gets value of a property by it's full path from given object
        public static object GetValueByPath(object obj, Type objectType, string propertyPath)
        {
            object obj2 = obj;
            Type type = objectType;
            string fullName = type.FullName;
            string text = propertyPath;
            if (fullName != null && text.StartsWith(fullName))
            {
                text = text.Replace(fullName + ".", "");
            }

            string[] array = text.Split(new char[1] { '.' });
            foreach (string name in array)
            {
                PropertyInfo property = type.GetProperty(name);
                if (property != null)
                {
                    if (obj2 != null)
                    {
                        obj2 = property.GetValue(obj2, null);
                    }

                    type = property.PropertyType;
                    continue;
                }

                obj2 = null;
                break;
            }

            return obj2;
        }

        //
        // Summary:
        //     Sets value of a property by it's full path on given object
        internal static void SetValueByPath(object obj, Type objectType, string propertyPath, object value)
        {
            Type type = objectType;
            string fullName = type.FullName;
            string text = propertyPath;
            if (text.StartsWith(fullName))
            {
                text = text.Replace(fullName + ".", "");
            }

            string[] array = text.Split(new char[1] { '.' });
            if (array.Length == 1)
            {
                objectType.GetProperty(array.First())!.SetValue(obj, value);
                return;
            }

            for (int i = 0; i < array.Length - 1; i++)
            {
                PropertyInfo? property = type.GetProperty(array[i]);
                obj = property!.GetValue(obj, null);
                type = property!.PropertyType;
            }

            type.GetProperty(array.Last())!.SetValue(obj, value);
        }

        //
        // Summary:
        //     Get all the constant values in the specified type (including the base type).
        //
        // Parameters:
        //   type:
        public static string[] GetPublicConstantsRecursively(Type type)
        {
            List<string> list = new List<string>();
            Recursively(list, type, 1);
            return list.ToArray();
            static void Recursively(List<string> constants, Type targetType, int currentDepth)
            {
                if (currentDepth <= 8)
                {
                    constants.AddRange(from x in targetType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)
                                       where x.IsLiteral && !x.IsInitOnly
                                       select x.GetValue(null)!.ToString());
                    Type[] nestedTypes = targetType.GetNestedTypes(BindingFlags.Public);
                    foreach (Type targetType2 in nestedTypes)
                    {
                        Recursively(constants, targetType2, currentDepth + 1);
                    }
                }
            }
        }
    }
}
