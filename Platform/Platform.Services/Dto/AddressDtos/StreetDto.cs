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
                StateName = street.City.State.Name,
                CityName = street.City.Name,
                StreetName = street.Name
            };
        
        public int Id { get; set; }
        
        [Platform(AttributesEnum.Grid  | AttributesEnum.Form)]
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
        public string StreetName { get; set; }
    }
}