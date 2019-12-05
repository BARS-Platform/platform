using System;
using System.Linq.Expressions;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;

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
                ApartmentNumber = apartment.Name
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

        [Platform(AttributesEnum.Grid)]
        [Label("Квартира")]
        public string ApartmentNumber { get; set; }
    }
}