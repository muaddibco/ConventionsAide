﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ConventionsAide.Core.Common.ExtensionMethods
{
    public static class TypeExtensionMethods
    {
        public static TAttribute GetAttribute<TAttribute>(this Type type, bool includeInherited = true)
            where TAttribute : Attribute => type.GetCustomAttributes(typeof(TAttribute), includeInherited).FirstOrDefault() as TAttribute;

        public static List<TAttribute> GetAttributeList<TAttribute>(this Type type, bool includeInherited = true)
                   where TAttribute : Attribute => (from attribute in type.GetCustomAttributes(typeof(TAttribute), includeInherited)
                                                    select attribute as TAttribute).ToList();

        public static string FullNameWithAssemblyPath(this Type type) => $"{type.FullName} in {type.Assembly.Location}";

        public static string NameWithGenericArgs(this Type type)
        {
            string typeName = type.Name;

            if (type.GenericTypeArguments != null && type.GenericTypeArguments.Any())
            {
                string args = string.Join(", ", type.GenericTypeArguments.Select(t => t.Name));
                typeName = $"{typeName}<{args}>";
            }

            return typeName;
        }

        public static string NamespaceAndName(this Type type) => $"{type.Namespace}.{type.Name}";
    }
}
