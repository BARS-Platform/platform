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
    public class PermissionController : BaseController
    {
        private readonly PermissionService _permissionService;
        
        public PermissionController(IRepository repository, PermissionService permissionService) : base(repository)
        {
            _permissionService = permissionService;
        }

        [Authorize(PermissionNamesHelper.PermissionView)]
        [HttpPost]
        public IActionResult GetAll([FromBody] ListParam listParam) => 
            HandleRequest(() => _permissionService.GetAll(listParam));

        [Authorize(PermissionNamesHelper.PermissionEdit)]
        [HttpPut]
        public IActionResult AddPermissionToRole(int roleId, int permissionId) =>
            HandleRequest(() => _permissionService.AddPermissionToRole(roleId, permissionId));

        [Authorize(PermissionNamesHelper.PermissionEdit)]
        [HttpPut]
        public IActionResult AddPermissionToRoleByName(int roleId, string permissionName) =>
            HandleRequest(() => _permissionService.AddPermissionToRole(roleId, permissionName));
    }
}