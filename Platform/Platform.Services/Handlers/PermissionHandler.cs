using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Platform.Services.Requirements;

namespace Platform.Services.Handlers
{
    // ReSharper disable once UnusedMember.Global
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (requirement.Name == "GridView")
            {
                context.Succeed(requirement);
            }
            
            return Task.CompletedTask;
        }
    }
}