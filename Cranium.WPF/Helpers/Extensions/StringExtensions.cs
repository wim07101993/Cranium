using System.Text;

namespace Cranium.WPF.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToUtf8(this string s) => Encoding.UTF8.GetBytes(s);
    }
}
