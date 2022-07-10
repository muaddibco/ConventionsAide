namespace ConventionsAide.Core.ObjectMapping
{
    public interface IObjectMapper
    {
        //
        // Summary:
        //     Gets the underlying Volo.Abp.ObjectMapping.IAutoObjectMappingProvider object
        //     that is used for auto object mapping.
        IAutoObjectMappingProvider AutoObjectMappingProvider { get; }

        //
        // Summary:
        //     Converts an object to another. Creates a new object of TDestination.
        //
        // Parameters:
        //   source:
        //     Source object
        //
        // Type parameters:
        //   TDestination:
        //     Type of the destination object
        //
        //   TSource:
        //     Type of the source object
        TDestination Map<TSource, TDestination>(TSource source);

        //
        // Summary:
        //     Execute a mapping from the source object to the existing destination object
        //
        // Parameters:
        //   source:
        //     Source object
        //
        //   destination:
        //     Destination object
        //
        // Type parameters:
        //   TSource:
        //     Source type
        //
        //   TDestination:
        //     Destination type
        //
        // Returns:
        //     Returns the same destination object after mapping operation
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}