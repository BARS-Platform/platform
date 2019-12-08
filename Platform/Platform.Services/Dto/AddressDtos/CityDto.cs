using System;
using System.Linq.Expressions;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;
using Platform.Services.Dto.Attributes;

namespace Platform.Services.Dto.AddressDtos
{
    [Label("Город")]
    public class CityDto : IEntityDto
    {
        public const string RefProperty = "cityName";

        public static readonly Expression<Func<City, CityDto>> ProjectionExpression = city =>
            new CityDto
            {
                Id = city.Id,
                CountryName = city.State.Country.Name,
                StateName = city.State.Name,
                CityName = city.Name
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
        public string CityName { get; set; }
    }
}