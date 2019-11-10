using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Platform.Services.Requirements;

namespace Platform.Services.Handlers
{
    // ReSharper disable once UnusedMember.Global
    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var roles = context.User.Claims.Where(x => x.Type == ClaimTypes.Role);

            if (roles.Any(x => x.Value == requirement.Name))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            
            context.Fail();
            return Task.CompletedTask;
        }
    }
}