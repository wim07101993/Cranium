using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shared.Extensions
{
    public static class StringExtensions
    {
        public static T DeserializeJson<T>(this string s)
            => JsonConvert.DeserializeObject<T>(s);

        public static async Task<T> DeserializeJsonAsync<T>(this Task<string> s)
            => JsonConvert.DeserializeObject<T>(await s);
    }
}