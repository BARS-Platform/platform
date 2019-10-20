using Platform.Models.Enums;
using System;

namespace Platform.Models.Attributes
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
