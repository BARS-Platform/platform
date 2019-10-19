using Platform.Models.Attributes;
using Platform.Models.Enums;

namespace Platform.Models
{
    [PlatformAttribute(AttributesEnum.Grid | AttributesEnum.Form)]
    public class CurrencyReferenceBook
    {
        [PlatformAttribute(AttributesEnum.Grid | AttributesEnum.Form)]
        public string Name { get; set; }

        [PlatformAttribute(AttributesEnum.Grid)]
        public string Code { get; set; }
    }
}
