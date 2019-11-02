//using System;
//using Platform.Configuration.Attributes;
//using Platform.Configuration.Enums;
//
//namespace Platform.Configuration
//{
//    public class WeatherForecast : IPlatformModel
//    {
//        public long Id { get; set; }
//
//        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
//        public DateTime Date { get; set; }
//
//        [Platform(AttributesEnum.Grid)]
//        public int TemperatureC { get; set; }
//
//        [Platform(AttributesEnum.Form)]
//        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//
//        public string Summary { get; set; }
//
//        [Platform(AttributesEnum.Form)]
//        public int MyProperty { get; set; }
//
//        public CurrencyReferenceBook Currency { get; set; }
//    }
//}
