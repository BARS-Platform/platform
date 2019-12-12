using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32.SafeHandles;
using Platform.Fatabase;
using Platform.Fodels.Models;
using Platform.Services.Common;

namespace Platform.Services.Services
{
	/// <summary>
	/// Service that generate JWT tokens.
	/// </summary>
	public class TokenService : IDisposable
	{
		private readonly IRepository _repository;

		public TokenService(IRepository repository)
		{
			_repository = repository;
		}

		public JwtSecurityToken GenerateToken(User user)
		{
			var key = JwtOptions.GetSymmetricSecurityKey();
			var now = DateTime.Now;
			var jwt = new JwtSecurityToken(
				JwtOptions.Issuer,
				JwtOptions.Audience,
				GetIdentity(user).Claims,
				now,
				now.AddMinutes(JwtOptions.Lifetime),
				new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
			return jwt;
		}

		private ClaimsIdentity GetIdentity(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Login),
				new Claim(ClaimTypes.Email, user.Email)
			};

			var userEntity = _repository.FindByPredicate<User>(x => x.Login == user.Login);

			var claimsWithRoles = _repository
				.FindAllByPredicate<UserRole>(x => x.User.Id == userEntity.Id)
				.Select(userRole => new Claim(ClaimTypes.Role, userRole.Role.RoleName))
				.ToList();

			claims.AddRange(claimsWithRoles);

			var claimsIdentity =
				new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
					ClaimsIdentity.DefaultRoleClaimType);
			return claimsIdentity;
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