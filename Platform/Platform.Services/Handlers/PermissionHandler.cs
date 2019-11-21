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
		private readonly IRepository _repository;

		public PermissionHandler(IRepository repository)
		{
			_repository = repository;
		}

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
			PermissionRequirement requirement)
		{
			var roles = context.User.Claims
				.Where(x => x.Type == ClaimTypes.Role);

			var roleIds = _repository
				.FindAllByPredicate<Role>(x => roles.Any(y => y.Value == x.RoleName))
				.Select(x => x.Id);

			var permissionIds = _repository
				.FindAllByPredicate<RolePermission>(x => roleIds.Any(id => id == x.Role.Id),
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