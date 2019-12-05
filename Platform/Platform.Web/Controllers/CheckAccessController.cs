using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Platform.Fodels.Attributes;
using Platform.Services.Helpers;
using Platform.Services.Services;

namespace Platform.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CheckAccessController : Controller
    {
        private readonly PermissionService _permissionService;

        public CheckAccessController(PermissionService permissionService) => _permissionService = permissionService;

        [HttpGet]
        public bool CheckAccess(string modelRoute)
        {
            var permissionIds = _permissionService
                .GetPermissions(HttpContext.User)
                .Select(x => x.PermissionId);
            
            var accessedRoutes = TypeHelper.GetAttributes<MenuAttribute>()
                .Where(x => permissionIds.Contains(x.Value.PermissionId.GetString()))
                .Select(x => x.Value.Link)
                .ToList();
            
            return accessedRoutes.Contains(modelRoute);
        }
    }
}