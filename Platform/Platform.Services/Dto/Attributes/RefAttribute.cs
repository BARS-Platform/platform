using System;

namespace Platform.Services.Dto.Attributes
{
    public class RefAttribute : Attribute
    {
        public string ModelName { get; set; }

        public string PropertyName { get; set; }

        public RefAttribute(string modelName, string propertyName)
        {
            ModelName = modelName;
            PropertyName = propertyName;
        }
    }
}
