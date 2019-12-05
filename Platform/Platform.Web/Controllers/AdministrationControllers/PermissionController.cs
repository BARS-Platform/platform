using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.Fatabase;
using Platform.Fodels.Models;
using Platform.Services.Common;
using Platform.Services.Dto;
using Platform.Services.Helpers;
using Platform.Web.Controllers.Base;

namespace Platform.Web.Controllers.AdministrationControllers
{
    [Authorize(PermissionNamesHelper.ViewAdmin)]
    [Route("api/[controller]/[action]")]
    public class PermissionController : BaseController<Permission>
    {
        public PermissionController(IRepository repository) : base(repository)
        {
        }

        [Authorize(PermissionNamesHelper.PermissionView)]
        public override IActionResult Get(int id) => base.Get(id);

        [Authorize(PermissionNamesHelper.PermissionView)]
        public override IActionResult GetAll([FromBody] ListParam listParam) =>
            Ok(Repository
                .GetAll<Permission>()
                .Select(PermissionDto.ProjectionExpression)
                .FormData(listParam));

        [Authorize(PermissionNamesHelper.PermissionEdit)]
        public override IActionResult Create(Permission entity) => base.Create(entity);

        [Authorize(PermissionNamesHelper.PermissionEdit)]
        public override IActionResult Update(Permission entity) => base.Update(entity);

        [Authorize(PermissionNamesHelper.PermissionEdit)]
        public override IActionResult Delete(Permission entity) => base.Delete(entity);
        
        

        [Authorize(PermissionNamesHelper.PermissionEdit)]
        [HttpPut]
        public void AddPermissionToRole(int roleId, int permissionId)
        {
            var role = Repository.Get<Role>(roleId);
            var permission = Repository.Get<Permission>(permissionId);
            Repository.Create(new RolePermission(role, permission));
        }

        [Authorize(PermissionNamesHelper.PermissionEdit)]
        [HttpPut]
        public void AddPermissionToRoleByName(int roleId, string permissionId)
        {
            var role = Repository.Get<Role>(roleId);
            var permission = Repository.FindByPredicate<Permission>(x => x.PermissionId == permissionId);
            Repository.Create(new RolePermission(role, permission));
        }
    }
}