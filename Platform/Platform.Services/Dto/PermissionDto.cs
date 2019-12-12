using System;
using System.Linq.Expressions;
using Platform.Fodels.Attributes;
using Platform.Fodels.Enums;
using Platform.Fodels.Models;

namespace Platform.Services.Dto
{
    [Label("Разрешение")]
    public class PermissionDto : IEntityDto
    {
        public static readonly Expression<Func<Permission, PermissionDto>> ProjectionExpression = permission =>
            new PermissionDto
            {
                Id = permission.Id,
                PermissionId = permission.PermissionId,
                Description = permission.Description
            };
        
        public int Id { get; set; }
        
        [Platform(AttributesEnum.Grid)]
        [Label("Название")]
        public string PermissionId { get; set; }
        
        [Platform(AttributesEnum.Grid)]
        [Label("Описание")]
        public string Description { get; set; }
    }
}