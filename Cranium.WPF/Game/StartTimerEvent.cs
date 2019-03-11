using Prism.Events;
using System;

namespace Cranium.WPF.Game
{
    public class StartTimerEvent : PubSubEvent<TimeSpan>
    {
    }
}
