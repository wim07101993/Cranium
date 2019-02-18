using Newtonsoft.Json;

namespace Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static string SerializeJson(this object o, Formatting formatting = Formatting.Indented)
            => JsonConvert.SerializeObject(o, formatting);

        public static T CastObject<T>(this object o)
            => (T)o;

        public static T Clone<T>(this T t)
           => t.SerializeJson().DeserializeJson<T>();
    }
}
