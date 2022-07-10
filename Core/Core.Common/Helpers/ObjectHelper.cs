using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ConventionsAide.Core.Common.Helpers
{
    public static class ObjectHelper
    {
        private static readonly ConcurrentDictionary<string, PropertyInfo> CachedObjectProperties = new ConcurrentDictionary<string, PropertyInfo>();

        public static void TrySetProperty<TObject, TValue>(TObject obj, Expression<Func<TObject, TValue>> propertySelector, Func<TValue> valueFactory, params Type[] ignoreAttributeTypes)
        {
            TrySetProperty(obj, propertySelector, (TObject x) => valueFactory(), ignoreAttributeTypes);
        }

        public static void TrySetProperty<TObject, TValue>(TObject obj, Expression<Func<TObject, TValue>> propertySelector, Func<TObject, TValue> valueFactory, params Type[] ignoreAttributeTypes)
        {
            string key = obj.GetType().FullName + "-" + $"{propertySelector}-" + ((ignoreAttributeTypes != null) ? ("-" + string.Join("-", ignoreAttributeTypes.Select((Type x) => x.FullName))) : "");
            DictionaryExtensions.GetOrAdd(CachedObjectProperties, key, delegate
            {
                if (propertySelector.Body.NodeType != ExpressionType.MemberAccess)
                {
                    return null;
                }

                MemberExpression memberExpression = propertySelector.Body.As<MemberExpression>();
                PropertyInfo propertyInfo = obj.GetType().GetProperties().FirstOrDefault((PropertyInfo x) => x.Name == memberExpression.Member.Name && x.GetSetMethod(nonPublic: true) != null);
                if (propertyInfo == null)
                {
                    return null;
                }

                return (ignoreAttributeTypes != null && ignoreAttributeTypes.Any((Type ignoreAttribute) => propertyInfo.IsDefined(ignoreAttribute, inherit: true))) ? null : propertyInfo;
            })?.SetValue(obj, valueFactory(obj));
        }
    }
}
