using System;
using Platform.Configuration.Enums;

namespace Platform.Configuration.Attributes
{
    public class PlatformAttribute : Attribute
    {
        public AttributesEnum Value { get; set; }

        public PlatformAttribute(AttributesEnum value)
        {
            Value = value;
        }
    }
}
