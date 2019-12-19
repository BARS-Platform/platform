using System;
using System.Linq.Expressions;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;
using Platform.Services.Dto.Attributes;

namespace Platform.Services.Dto.AddressDtos
{
    [Label("Дома")]
    public class HouseDto : IEntityDto
    {
        public const string RefProperty = "houseNumber";
        
        public static readonly Expression<Func<House, HouseDto>> ProjectionExpression = house =>
            new HouseDto
            {
                Id = house.Id,
                CountryName = house.Street.City.State.Country.Name,
                CountryId = house.Street.City.State.Country.Id,
                StateName = house.Street.City.State.Name,
                StateId = house.Street.City.State.Id,
                CityName = house.Street.City.Name,
                CityId = house.Street.City.Id,
                StreetName = house.Street.Name,
                StreetId = house.Street.Id,
                HouseNumber = house.Name
            };
        
        public int Id { get; set; }
        
        [Platform(AttributesEnum.Grid)]
        [Label("Страна")]
        public string CountryName { get; set; }

        [Platform(AttributesEnum.Form)]
        [Label("Страна")]
        [Ref(nameof(Country), "GetAll", CountryDto.RefProperty)]
        public int CountryId { get; set; }

        [Platform(AttributesEnum.Grid)]
        [Label("Регион")]
        public string StateName { get; set; }

        [Platform(AttributesEnum.Form)]
        [Label("Регион")]
        [Ref(nameof(State), "GetAll", StateDto.RefProperty)]
        public int StateId { get; set; }

        [Platform(AttributesEnum.Grid)]
        [Label("Город")]
        public string CityName { get; set; }

        [Platform(AttributesEnum.Form)]
        [Label("Город")]
        [Ref(nameof(City), "GetAll", CityDto.RefProperty)]
        public int CityId { get; set; }

        [Platform(AttributesEnum.Grid)]
        [Label("Улица")]
        public string StreetName { get; set; }

        [Platform(AttributesEnum.Form)]
        [Label("Улица")]
        [Ref(nameof(Street), "GetAll", StreetDto.RefProperty)]
        public int StreetId { get; set; }

        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
        [Label("Дом")]
        public string HouseNumber { get; set; }
    }
}