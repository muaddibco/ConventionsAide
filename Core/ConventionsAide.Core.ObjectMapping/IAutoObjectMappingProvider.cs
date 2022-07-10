﻿namespace ConventionsAide.Core.ObjectMapping
{
    public interface IAutoObjectMappingProvider
    {
        TDestination Map<TSource, TDestination>(object source);

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
