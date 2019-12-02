using System;
using System.Linq.Expressions;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;

namespace Platform.Services.Dto.AddressDtos
{
    [Label("Город")]
    public class CityDto : IEntityDto
    {
        public static readonly Expression<Func<City, CityDto>> ProjectionExpression = city =>
            new CityDto
            {
                Id = city.Id,
                CountryName = city.State.Country.Name,
                StateName = city.State.Name,
                CityName = city.Name
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
    }
}