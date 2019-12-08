using System;
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
        public static readonly Expression<Func<State, StateDto>> ProjectionExpression = state =>
            new StateDto
            {
                Id = state.Id,
                CountryName = state.Country.Name,
                StateName = state.Name,
                CountryId = state.Country.Id
            };
        
        public int Id { get; set; }
        
        [Platform(AttributesEnum.Grid)]
        [Label("Страна")]
        public string CountryName { get; set; }

        [Platform(AttributesEnum.Grid | AttributesEnum.Form)]
        [Label("Регион")]
        public string StateName { get; set; }

        [Platform(AttributesEnum.Form)]
        [Ref(nameof(Country),nameof(CountryName))]
        public int CountryId { get; set; }
    }
}