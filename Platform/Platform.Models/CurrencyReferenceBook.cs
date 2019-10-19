using Platform.Models.Attributes;
using Platform.Models.Enums;
using Platform.Models.Interfaces;

namespace Platform.Models
{
    [PlatformAttribute(AttributesEnum.Grid | AttributesEnum.Form)]
    public class CurrencyReferenceBook : IPlatformModel
    {
        public long Id { get; set; }

        [PlatformAttribute(AttributesEnum.Grid | AttributesEnum.Form)]
        public string Name { get; set; }

        [PlatformAttribute(AttributesEnum.Grid)]
        public string Code { get; set; }
    }
}
