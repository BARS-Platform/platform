using System;
using System.Linq.Expressions;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;

namespace Platform.Services.Dto.AddressDtos
{
    [Label("Улица")]
    public class StreetDto : IEntityDto
    {
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
    }
}