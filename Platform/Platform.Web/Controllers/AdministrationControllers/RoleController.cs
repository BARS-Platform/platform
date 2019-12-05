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
    public class RoleController : BaseController<Role>
    {
        public RoleController(IRepository repository)
            : base(repository)
        {
        }

        [Authorize(PermissionNamesHelper.RoleView)]
        public override IActionResult Get(int id) => base.Get(id);

        [Authorize(PermissionNamesHelper.RoleView)]
        public override IActionResult GetAll([FromBody] ListParam listParam) =>
            Ok(Repository.GetAll<Role>().Select(RoleDto.ProjectionExpression).FormData(listParam));

        [Authorize(PermissionNamesHelper.RoleEdit)]
        public override IActionResult Create(Role entity) => base.Create(entity);

        [Authorize(PermissionNamesHelper.RoleEdit)]
        public override IActionResult Update(Role entity) => base.Update(entity);

        [Authorize(PermissionNamesHelper.RoleEdit)]
        public override IActionResult Delete(Role entity) => base.Delete(entity);

        [Authorize(PermissionNamesHelper.RoleEdit)]
        [HttpPut]
        public void AddRoleToUser(int userId, int roleId)
        {
            var user = Repository.Get<User>(userId);
            var role = Repository.Get<Role>(roleId);
            Repository.Create(new UserRole(user, role));
        }

        [Authorize(PermissionNamesHelper.RoleEdit)]
        [HttpPut]
        public void AddRoleToUserByName(int userId, string roleName)
        {
            var user = Repository.Get<User>(userId);
            var role = Repository.FindByPredicate<Role>(x => x.RoleName == roleName);
            Repository.Create(new UserRole(user, role));
        }
    }
}