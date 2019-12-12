using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using Platform.Fatabase;
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
			var roleNames = context.User.Claims
				.Where(x => x.Type == ClaimTypes.Role)
				.Select(x => x.Value);

			var roleIds = _repository
				.FindAllByPredicate<Role>(x => roleNames.Contains(x.RoleName))
				.Select(x => x.Id)
				.ToList();

			var permissionIds = _repository
				.FindAllByPredicate<RolePermission>(x => roleIds.Contains(x.Role.Id))
				.Select(x => x.Permission)
				.Select(x => x.PermissionId)
				.ToList();

			if (permissionIds.Contains(requirement.Name))
			{
				context.Succeed(requirement);
				return Task.CompletedTask;
			}

			context.Fail();
			return Task.CompletedTask;
		}
		
		#region IDisposable 

		private bool _disposed;
		private readonly SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);

		public void Dispose()
		{
			_repository.Dispose();
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			if (disposing)
				_handle.Dispose();

			_disposed = true;
		}

		#endregion
	}
}