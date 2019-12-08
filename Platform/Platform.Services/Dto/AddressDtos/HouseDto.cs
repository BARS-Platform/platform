using System;
using System.Linq.Expressions;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;
using Platform.Services.Dto.Attributes;

namespace Platform.Services.Dto.AddressDtos
{
    [Label("Дом")]
    public class HouseDto : IEntityDto
    {
        public const string RefProperty = "houseNumber";
        
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
        
        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
        [Label("Страна")]
        [Ref(nameof(Country), CountryDto.RefProperty)]
        public string CountryName { get; set; }

        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
        [Label("Регион")]
        [Ref(nameof(State), StateDto.RefProperty)]
        public string StateName { get; set; }

        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
        [Label("Город")]
        [Ref(nameof(City), CityDto.RefProperty)]
        public string CityName { get; set; }

        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
        [Label("Улица")]
        [Ref(nameof(Street), StreetDto.RefProperty)]
        public string StreetName { get; set; }

        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
        [Label("Дом")]
        public string HouseNumber { get; set; }
    }
}