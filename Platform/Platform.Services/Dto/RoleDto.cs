using System;
using System.Linq.Expressions;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;
using Platform.Fodels.Models;

namespace Platform.Services.Dto
{
    [Label("Роль")]
    public class RoleDto : IEntityDto
    {
        public static readonly Expression<Func<Role, RoleDto>> ProjectionExpression = role =>
            new RoleDto
            {
                Id = role.Id,
                RoleName = role.RoleName,
                Description = role.Description
            };
        
        public int Id { get; set; }

        [Platform(AttributesEnum.Grid)]
        [Label("Название")]
        public string RoleName { get; set; }
        
        [Platform(AttributesEnum.Grid)]
        [Label("Описание")]
        public string Description { get; set; }
    }
}