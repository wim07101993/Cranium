using Newtonsoft.Json;

namespace Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static string SerializeJson(this object o, Formatting formatting = Formatting.Indented)
            => JsonConvert.SerializeObject(o, formatting);
    }
}
