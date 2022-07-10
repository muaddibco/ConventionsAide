using MassTransit.Clients;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace ConventionsAide.Core.Communication
{
    public class MessageResponseContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);

            if (member.DeclaringType == typeof(MessageResponse<>) && (prop.PropertyName == "Request" || prop.PropertyName == "Payload"))
            {
                prop.Writable = true;
            }

            return prop;
        }
    }
}
