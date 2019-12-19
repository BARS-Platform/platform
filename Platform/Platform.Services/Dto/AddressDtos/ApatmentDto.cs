using System;
using System.Linq.Expressions;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;
using Platform.Services.Dto.Attributes;

namespace Platform.Services.Dto.AddressDtos
{
    [Label("Квартиры")]
    [Ref(nameof(Apartment), "GetAll")]
    public class ApartmentDto : IEntityDto
    {
        public static readonly Expression<Func<Apartment, ApartmentDto>> ProjectionExpression = apartment =>
            new ApartmentDto
            {
                Id = apartment.Id,
                CountryName = apartment.House.Street.City.State.Country.Name,
                CountryId = apartment.House.Street.City.State.Country.Id,
                StateName = apartment.House.Street.City.State.Name,
                StateId = apartment.House.Street.City.State.Id,
                CityName = apartment.House.Street.City.Name,
                CityId = apartment.House.Street.City.Id,
                StreetName = apartment.House.Street.Name,
                StreetId = apartment.House.Street.Id,
                HouseNumber = apartment.House.Name,
                HouseId = apartment.House.Id,
                ApartmentNumber = Convert.ToInt32(apartment.Name)
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

        [Platform(AttributesEnum.Grid)]
        [Label("Дом")]
        public string HouseNumber { get; set; }

        [Platform(AttributesEnum.Form)]
        [Label("Дом")]
        [Ref(nameof(House), "GetAll", HouseDto.RefProperty)]
        public int HouseId { get; set; }

        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
        [Label("Квартира")]
        public int ApartmentNumber { get; set; }
    }
}