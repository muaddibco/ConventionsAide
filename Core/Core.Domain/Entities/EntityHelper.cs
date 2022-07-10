﻿using ConventionsAide.Core.Common.Helpers;
using ConventionsAide.Core.Common.Reflection;
using PostSharp.Patterns.Contracts;
using System.Linq.Expressions;
using System.Reflection;

namespace ConventionsAide.Core.Domain.Entities
{
    public static class EntityHelper
    {
        public static bool EntityEquals(IEntity entity1, IEntity entity2)
        {
            if (entity1 == null || entity2 == null)
            {
                return false;
            }

            //Same instances must be considered as equal
            if (ReferenceEquals(entity1, entity2))
            {
                return true;
            }

            //Must have a IS-A relation of types or must be same type
            var typeOfEntity1 = entity1.GetType();
            var typeOfEntity2 = entity2.GetType();
            if (!typeOfEntity1.IsAssignableFrom(typeOfEntity2) && !typeOfEntity2.IsAssignableFrom(typeOfEntity1))
            {
                return false;
            }

            //Transient objects are not considered as equal
            if (HasDefaultKeys(entity1) && HasDefaultKeys(entity2))
            {
                return false;
            }

            var entity1Keys = entity1.GetKeys();
            var entity2Keys = entity2.GetKeys();

            if (entity1Keys.Length != entity2Keys.Length)
            {
                return false;
            }

            for (var i = 0; i < entity1Keys.Length; i++)
            {
                var entity1Key = entity1Keys[i];
                var entity2Key = entity2Keys[i];

                if (entity1Key == null)
                {
                    if (entity2Key == null)
                    {
                        //Both null, so considered as equals
                        continue;
                    }

                    //entity2Key is not null!
                    return false;
                }

                if (entity2Key == null)
                {
                    //entity1Key was not null!
                    return false;
                }

                if (TypeHelper.IsDefaultValue(entity1Key) && TypeHelper.IsDefaultValue(entity2Key))
                {
                    return false;
                }

                if (!entity1Key.Equals(entity2Key))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsEntity([NotNull] Type type)
        {
            return typeof(IEntity).IsAssignableFrom(type);
        }

        public static void CheckEntity([NotNull] Type type)
        {
            if (!IsEntity(type))
            {
                throw new Exception($"Given {nameof(type)} is not an entity: {type.AssemblyQualifiedName}. It must implement {typeof(IEntity).AssemblyQualifiedName}.");
            }
        }

        public static bool IsEntityWithId([NotNull] Type type)
        {
            foreach (var interfaceType in type.GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HasDefaultId<TKey>(IEntity<TKey> entity)
        {
            if (EqualityComparer<TKey>.Default.Equals(entity.Id, default))
            {
                return true;
            }

            //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
            if (typeof(TKey) == typeof(int))
            {
                return Convert.ToInt32(entity.Id) <= 0;
            }

            if (typeof(TKey) == typeof(long))
            {
                return Convert.ToInt64(entity.Id) <= 0;
            }

            return false;
        }

        private static bool IsDefaultKeyValue(object value)
        {
            if (value == null)
            {
                return true;
            }

            var type = value.GetType();

            //Workaround for EF Core since it sets int/long to min value when attaching to DbContext
            if (type == typeof(int))
            {
                return Convert.ToInt32(value) <= 0;
            }

            if (type == typeof(long))
            {
                return Convert.ToInt64(value) <= 0;
            }

            return TypeHelper.IsDefaultValue(value);
        }

        public static bool HasDefaultKeys([NotNull] IEntity entity)
        {
            foreach (var key in entity.GetKeys())
            {
                if (!IsDefaultKeyValue(key))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Tries to find the primary key type of the given entity type.
        /// May return null if given type does not implement <see cref="IEntity{TKey}"/>
        /// </summary>
        public static Type? FindPrimaryKeyType<TEntity>()
            where TEntity : IEntity
        {
            return FindPrimaryKeyType(typeof(TEntity));
        }

        /// <summary>
        /// Tries to find the primary key type of the given entity type.
        /// May return null if given type does not implement <see cref="IEntity{TKey}"/>
        /// </summary>
        public static Type? FindPrimaryKeyType([NotNull] Type entityType)
        {
            if (!typeof(IEntity).IsAssignableFrom(entityType))
            {
                throw new Exception(
                    $"Given {nameof(entityType)} is not an entity. It should implement {typeof(IEntity).AssemblyQualifiedName}!");
            }

            foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }

            return null;
        }

        public static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId<TEntity, TKey>(TKey id)
            where TEntity : IEntity<TKey>
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");
            var idValue = Convert.ChangeType(id, typeof(TKey));
            Expression<Func<object>> closure = () => idValue;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);
            var lambdaBody = Expression.Equal(leftExpression, rightExpression);
            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        public static void TrySetId<TKey>(
            IEntity<TKey> entity,
            Func<TKey> idFactory)
        {
            ObjectHelper.TrySetProperty(
                entity,
                x => x.Id,
                idFactory,
                Array.Empty<Type>());
        }
    }
}
