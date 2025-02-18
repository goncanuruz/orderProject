using Newtonsoft.Json;

namespace OrderProject.Application.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, settings: new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
        public static T ToObject<T>(this string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
    }
}
