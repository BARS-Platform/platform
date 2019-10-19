using System;
using Platform.Models.Attributes;

namespace Platform.Models
{
    public class WeatherForecast
    {
        [GridAttribute]
        [FormAttribute]
        public DateTime Date { get; set; }

        [GridAttribute]
        public int TemperatureC { get; set; }
        
        [GridAttribute]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

        [FormAttribute]
        public int MyProperty { get; set; }

        public CurrencyReferenceBook Currency { get; set; }
    }
}
