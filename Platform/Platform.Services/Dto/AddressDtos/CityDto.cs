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
                CountryId = city.State.Country.Id,
                StateName = city.State.Name,
                StateId = city.State.Id,
                CityName = city.Name
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

        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
        [Label("Город")]
        public string CityName { get; set; }
    }
}