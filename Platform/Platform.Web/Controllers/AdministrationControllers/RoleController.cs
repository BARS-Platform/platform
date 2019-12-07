using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.Fatabase;
using Platform.Services.Common;
using Platform.Services.Helpers;
using Platform.Services.Services;
using Platform.Web.Controllers.Base;

namespace Platform.Web.Controllers.AdministrationControllers
{
    [Authorize(PermissionNamesHelper.ViewAdmin)]
    [Route("api/[controller]/[action]")]
    public class RoleController : BaseController
    {
        private readonly RoleService _roleService;
        
        public RoleController(IRepository repository, RoleService roleService)
            : base(repository) =>
            _roleService = roleService;

        [Authorize(PermissionNamesHelper.RoleView)]
        [HttpPost]
        public IActionResult GetAll([FromBody] ListParam listParam) =>
            HandleRequest(() => _roleService.GetAll(listParam));

        [Authorize(PermissionNamesHelper.RoleEdit)]
        [HttpPut]
        public IActionResult AddRoleToUser(int userId, int roleId) =>
            HandleRequest(() => _roleService.AddRoleToUser(userId, roleId));

        [Authorize(PermissionNamesHelper.RoleEdit)]
        [HttpPut]
        public IActionResult AddRoleToUserByName(int userId, string roleName) =>
            HandleRequest(() => _roleService.AddRoleToUser(userId, roleName));
    }
}