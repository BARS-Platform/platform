using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Platform.Fatabase;
using Platform.Fodels.Interfaces;
using Platform.Fodels.Models;
using Platform.Services.Requirements;

namespace Platform.Services.Handlers
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<RolePermission> _rolePermissionRepository;

        public PermissionHandler(IRepository<Role> roleRepository, IRepository<RolePermission> rolePermissionRepository)
        {
            _roleRepository = roleRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var roles = context.User.Claims
                .Where(x => x.Type == ClaimTypes.Role);

            var roleIds = _roleRepository
                .FindAllByPredicate(x => roles.Any(y => y.Value == x.RoleName))
                .Select(x => ((IPlatformModel) x).Id);

            var permissionIds = _rolePermissionRepository
                .FindAllByPredicate(x => roleIds.Any(id => id == ((IPlatformModel) x.Role).Id),
                    query => query
                        .Include(x => x.Role)
                        .Include(x => x.Permission))
                .Select(x => x.Permission)
                .Select(x => x.PermissionId);

            if (permissionIds.Contains(requirement.Name))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}