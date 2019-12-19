using System;
using System.Linq;
using System.Linq.Expressions;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;
using Platform.Services.Common;
using Platform.Services.Dto.Attributes;

namespace Platform.Services.Dto.AddressDtos
{
    [Label("Регионы")]
    public class StateDto : IEntityDto
    {
        public const string RefProperty = "stateName";
        
        public static readonly Expression<Func<State, StateDto>> ProjectionExpression = state =>
            new StateDto
            {
                Id = state.Id,
                CountryName = state.Country.Name,
                CountryId = state.Country.Id,
                StateName = state.Name
            };
        
        public int Id { get; set; }
        
        [Platform(AttributesEnum.Grid)]
        [Label("Страна")]
        public string CountryName { get; set; }

        [Platform(AttributesEnum.Form)]
        [Label("Страна")]
        [Ref(nameof(Country), "GetAll", CountryDto.RefProperty)]
        public int CountryId { get; set; }

        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
        [Label("Регион")]
        public string StateName { get; set; }
    }
}