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
    [Label("Регион")]
    public class StateDto : IEntityDto
    {
        public const string RefProperty = "stateName";
        
        public static readonly Expression<Func<State, StateDto>> ProjectionExpression = state =>
            new StateDto
            {
                Id = state.Id,
                CountryName = state.Country.Name,
                StateName = state.Name
            };
        
        public int Id { get; set; }
        
        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
        [Label("Страна")]
        [Ref(nameof(Country), CountryDto.RefProperty)]
        public string CountryName { get; set; }

        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
        [Label("Регион")]
        public string StateName { get; set; }
    }
}