using System.Threading.Tasks;

namespace Cranium.WPF.Helpers
{
    public delegate Task AsyncEventHandler<T>(T sender);
}
