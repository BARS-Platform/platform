using System;
using System.Linq.Expressions;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;

namespace Platform.Services.Dto.AddressDtos
{
    [Label("Страна")]
    public class CountryDto : IEntityDto
    {
        public const string RefProperty = "countryName";
        
        public static readonly Expression<Func<Country, CountryDto>> ProjectionExpression = country =>
            new CountryDto
            {
                Id = country.Id,
                CountryName = country.Name
            };
        
        public int Id { get; set; }
        
        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
        [Label("Страна")]
        public string CountryName { get; set; }
    }
}