using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Platform.Web.Controllers
{
	[Route("[controller]/[action]")]
	public class AuthentificationController : Controller
	{
		/// <summary>
		/// Used to log in and receive new JWT.
		/// </summary>
		/// <returns>JWT ready for header format, example : Bearer *token*</returns>
		[HttpGet]
		public IActionResult LogIn(string login, string password)
		{
			// validate user credentials (custom validator?)
			 
			// generate token 
			// return
			return StatusCode(500);
		}

		[HttpPost]
		public IActionResult Register(string login, string password)
		{
			// register user
			// generate token
			// return
			return StatusCode(500);
		}

		[HttpGet]
		public IActionResult GetToken()
		{
			return Ok(new
			{
				Token = new JwtSecurityTokenHandler().WriteToken(GenerateToken())
			});
		}

		private JwtSecurityToken GenerateToken()
		{
			var key = JwtOptions.GetSymmetricSecurityKey();
			var now = DateTime.Now;
			var jwt = new JwtSecurityToken(
				JwtOptions.Issuer,
				JwtOptions.Audience,
				GetIdentity(string.Empty).Claims,
				now,
				now.AddMinutes(JwtOptions.Lifetime),
				new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
			return jwt;
		}

		private ClaimsIdentity GetIdentity(string login)
		{
			var user = new {Login = "d4", Role = new {Name = "admin"}}; // get user by login from db
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