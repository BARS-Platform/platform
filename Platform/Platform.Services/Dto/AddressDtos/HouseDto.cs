using System;
using System.Linq.Expressions;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;

namespace Platform.Services.Dto.AddressDtos
{
    [Label("Дом")]
    public class HouseDto : IEntityDto
    {
        public static readonly Expression<Func<House, HouseDto>> ProjectionExpression = house =>
            new HouseDto
            {
                Id = house.Id,
                CountryName = house.Street.City.State.Country.Name,
                StateName = house.Street.City.State.Name,
                CityName = house.Street.City.Name,
                StreetName = house.Street.Name,
                HouseNumber = house.Name
            };
        
        public int Id { get; set; }
        
        [Platform(AttributesEnum.Grid)]
        [Label("Страна")]
        public string CountryName { get; set; }

        [Platform(AttributesEnum.Grid)]
        [Label("Регион")]
        public string StateName { get; set; }

        [Platform(AttributesEnum.Grid)]
        [Label("Город")]
        public string CityName { get; set; }

        [Platform(AttributesEnum.Grid)]
        [Label("Улица")]
        public string StreetName { get; set; }

        [Platform(AttributesEnum.Grid)]
        [Label("Дом")]
        public string HouseNumber { get; set; }
    }
}