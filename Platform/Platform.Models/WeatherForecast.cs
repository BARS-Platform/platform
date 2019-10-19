using System;
using Platform.Models.Attributes;
using Platform.Models.Enums;

namespace Platform.Models
{
    public class WeatherForecast
    {
        [PlatformAttribute(AttributesEnum.Grid | AttributesEnum.Form)]
        public DateTime Date { get; set; }

        [PlatformAttribute(AttributesEnum.Grid)]
        public int TemperatureC { get; set; }

        [PlatformAttribute(AttributesEnum.Form)]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

        [PlatformAttribute(AttributesEnum.Form)]
        public int MyProperty { get; set; }

        public CurrencyReferenceBook Currency { get; set; }
    }
}
