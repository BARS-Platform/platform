using Platform.Fodels.Interfaces;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;

namespace Platform.Services.Dto
{
    [Label("Квартира")]
    public class AddressDto : IEntityBase
    {
        public int Id { get; set; }

        [Platform(AttributesEnum.Grid)]
        [Label("Страна")]
        public string CountryName { get; set; }

        [Platform(AttributesEnum.Grid)]
        public string StateName { get; set; }

        [Platform(AttributesEnum.Grid)]
        public string CityName { get; set; }

        [Platform(AttributesEnum.Grid)]
        public string StreetName { get; set; }

        [Platform(AttributesEnum.Grid)]
        public string HouseNumber { get; set; }

        [Platform(AttributesEnum.Grid)]
        public string ApartmentNumber { get; set; }
    }
}
