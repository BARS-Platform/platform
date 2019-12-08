using System;
using System.Linq.Expressions;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;
using Platform.Services.Dto.Attributes;

namespace Platform.Services.Dto.AddressDtos
{
    [Label("Квартира")]
    public class ApartmentDto : IEntityDto
    {
        public static readonly Expression<Func<Apartment, ApartmentDto>> ProjectionExpression = apartment =>
            new ApartmentDto
            {
                Id = apartment.Id,
                CountryName = apartment.House.Street.City.State.Country.Name,
                StateName = apartment.House.Street.City.State.Name,
                CityName = apartment.House.Street.City.Name,
                StreetName = apartment.House.Street.Name,
                HouseNumber = apartment.House.Name,
                //TODO Исправить тип в модели
                ApartmentNumber = Convert.ToInt32(apartment.Name)
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
        [Ref(nameof(House), HouseDto.RefProperty)]
        public string HouseNumber { get; set; }

        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
        [Label("Квартира")]
        public int ApartmentNumber { get; set; }
    }
}