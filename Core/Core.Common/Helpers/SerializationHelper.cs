using Newtonsoft.Json;
using System;
using System.Text;

namespace ConventionsAide.Core.Common.Helpers
{
    public static class SerializationHelper
    {
        public static string ToBase64(object input)
        {
            string json = JsonConvert.SerializeObject(input);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        }

        public static T FromBase64<T>(string input)
        {
            var byteArr = Convert.FromBase64String(input);
            var json = Encoding.UTF8.GetString(byteArr);
            return JsonConvert.DeserializeObject<T>(json,
                new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error });
        }
    }
}
