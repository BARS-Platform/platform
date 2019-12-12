using Microsoft.AspNetCore.Mvc;
using Platform.Fodels.Entities;
using Platform.Services.Services;

namespace Platform.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class MenuController : Controller
    {
        private readonly PermissionService _permissionService;
        private readonly MenuService _menuService;
        
        public MenuController(PermissionService permissionService, MenuService menuService)
        {
            _permissionService = permissionService;
            _menuService = menuService;
        }
        
        /// <summary>
        /// Метод для получения меню.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public MenuEntity GetMenu()
        {
            var permissions = _permissionService.GetPermissions(HttpContext.User);
            return _menuService.GenerateMenu(permissions);
        }
    }
}
