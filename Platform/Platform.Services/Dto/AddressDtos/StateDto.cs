using System;
using System.Linq.Expressions;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;

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
                StateName = state.Name
            };
        
        public int Id { get; set; }
        
        [Platform(AttributesEnum.Grid)]
        [Label("Страна")]
        public string CountryName { get; set; }

        [Platform(AttributesEnum.Grid)]
        [Label("Регион")]
        public string StateName { get; set; }
    }
}