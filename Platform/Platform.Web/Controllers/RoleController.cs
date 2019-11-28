using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.Fatabase;
using Platform.Fodels.Models;
using Platform.Services.Helpers;
using Platform.Web.Controllers.Base;

namespace Platform.Web.Controllers
{
    [Authorize(PermissionNamesHelper.ViewAdmin)]
    [Route("api/[controller]/[action]")]
    public class RoleController : BaseController<Role>
    {
        public RoleController(IRepository repository)
            :base (repository)
        {
        }

        [Authorize(PermissionNamesHelper.RoleView)]
        public override Role Get(int id) => base.Get(id);

        [Authorize(PermissionNamesHelper.RoleView)]
        public override IEnumerable<Role> GetAll() => base.GetAll();

        [Authorize(PermissionNamesHelper.RoleEdit)]
        public override Role Create(Role entity) => base.Create(entity);

        [Authorize(PermissionNamesHelper.RoleEdit)]
        public override Role Update(Role entity) => base.Update(entity);

        [Authorize(PermissionNamesHelper.RoleEdit)]
        public override bool Delete(Role entity) => base.Delete(entity);

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
