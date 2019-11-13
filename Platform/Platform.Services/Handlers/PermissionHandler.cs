using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Platform.Fatabase;
using Platform.Fodels.Models;
using Platform.Services.Requirements;

namespace Platform.Services.Handlers
{
    // ReSharper disable once UnusedMember.Global
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IRepository<Role> _roleRepository;

        public PermissionHandler(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var roles = context.User.Claims
                .Where(x => x.Type == ClaimTypes.Role);

            var permissions = _roleRepository
                .FindAllByPredicate(x => roles.Any(y => y.Value == x.RoleName))
                .Select(x => x.Permissions.ToList())
                .SelectMany(x => x.Select(y => y.PermissionId));

            if (permissions.Contains(requirement.Name))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}