using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Platform.Domain.Common;

namespace Platform.Domain.Services
{
	/// <summary>
	/// Service that generate JWT tokens.
	/// </summary>
	public class TokenService
	{
		internal JwtSecurityToken GenerateToken(string login)
		{
			var key = JwtOptions.GetSymmetricSecurityKey();
			var now = DateTime.Now;
			var jwt = new JwtSecurityToken(
				JwtOptions.Issuer,
				JwtOptions.Audience,
				GetIdentity(login).Claims,
				now,
				now.AddMinutes(JwtOptions.Lifetime),
				new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
			return jwt;
		}

		private ClaimsIdentity GetIdentity(string login)
		{
			var user = new {Login = login, Role = new {Name = "admin"}}; // get user by login from db
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Login)
			};
			if (user.Role != null)
				claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));

			var claimsIdentity =
				new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
					ClaimsIdentity.DefaultRoleClaimType);
			return claimsIdentity;
		}
	}
}