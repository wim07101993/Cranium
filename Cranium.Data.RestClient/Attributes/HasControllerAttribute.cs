using System;
using System.Runtime.CompilerServices;

namespace Cranium.Data.RestClient.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HasControllerAttribute : Attribute
    {
        public HasControllerAttribute([CallerMemberName] string controllerName = null)
        {
            ControllerName = $"{controllerName}s";
        }

        public HasControllerAttribute(string controllerName, bool hasController)
        {
            ControllerName = controllerName;
        }

        public string ControllerName { get; }
    }
}
