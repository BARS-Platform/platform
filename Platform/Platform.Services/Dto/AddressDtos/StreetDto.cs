using System;
using System.Linq.Expressions;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;
using Platform.Services.Dto.Attributes;

namespace Platform.Services.Dto.AddressDtos
{
    [Label("Улица")]
    public class StreetDto : IEntityDto
    {
        public const string RefProperty = "streetName";
        
        public static readonly Expression<Func<Street, StreetDto>> ProjectionExpression = street =>
            new StreetDto
            {
                Id = street.Id,
                CountryName = street.City.State.Country.Name,
                CountryId = street.City.State.Country.Id,
                StateName = street.City.State.Name,
                StateId = street.City.State.Id,
                CityName = street.City.Name,
                CityId = street.City.Id,
                StreetName = street.Name
            };
        
        public int Id { get; set; }
        
        [Platform(AttributesEnum.Grid)]
        [Label("Страна")]
        public string CountryName { get; set; }

        [Platform(AttributesEnum.Form)]
        [Label("Страна")]
        [Ref(nameof(Country), CountryDto.RefProperty)]
        public int CountryId { get; set; }

        [Platform(AttributesEnum.Grid)]
        [Label("Регион")]
        public string StateName { get; set; }

        [Platform(AttributesEnum.Form)]
        [Label("Регион")]
        [Ref(nameof(State), StateDto.RefProperty)]
        public int StateId { get; set; }

        [Platform(AttributesEnum.Grid)]
        [Label("Город")]
        public string CityName { get; set; }

        [Platform(AttributesEnum.Form)]
        [Label("Город")]
        [Ref(nameof(City), CityDto.RefProperty)]
        public int CityId { get; set; }

        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
        [Label("Улица")]
        public string StreetName { get; set; }
    }
}