using ConventionsAide.Core.Common.ExtensionMethods;
using MassTransit.Topology;

namespace ConventionsAide.Core.Communication
{
    public class KebabEntityNameFormatter : IEntityNameFormatter
    {
        public string FormatEntityName<T>()
        {
            string typeName = typeof(T).Name;

            if (typeof(CommandMessageBase).IsAssignableFrom(typeof(T)) && typeof(T).GenericTypeArguments.Length > 0)
            {
                typeName = typeof(T).GenericTypeArguments[0].Name;
            }

            if (typeName.StartsWith("I") && typeName.Length > 1 && char.IsUpper(typeName[1]))
            {
                typeName = typeName[1..];
            }

            typeName = typeName.PascalToKebabCase();

            return typeName;
        }
    }
}
