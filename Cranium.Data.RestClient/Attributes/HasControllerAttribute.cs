using System;

namespace Cranium.Data.RestClient.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HasControllerAttribute : Attribute
    {
        public HasControllerAttribute(string controllerName, bool addS = true)
        {
            ControllerName = addS
                ? $"{controllerName}s"
                : controllerName;
        }

        public string ControllerName { get; }
    }
}