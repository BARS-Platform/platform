using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Platform.Domain.Common;
using Platform.Fodels;
using Platform.Fodels.Models;

namespace Platform.Domain.Services
{
	/// <summary>
	/// Service that generate JWT tokens.
	/// </summary>
	public class TokenService
	{
		internal JwtSecurityToken GenerateToken(User user)
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
			var userWithRole = new {user.Login, user.Email, Role = new {Name = "admin"}}; //add role to User model?
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Login),
				new Claim(ClaimTypes.Email, user.Email)
			};
			if (userWithRole.Role != null)
				claims.Add(new Claim(ClaimTypes.Role, userWithRole.Role.Name));

			var claimsIdentity =
				new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
					ClaimsIdentity.DefaultRoleClaimType);
			return claimsIdentity;
		}
	}
}