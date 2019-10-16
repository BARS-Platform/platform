using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Platform.Web.Controllers
{
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

		private ClaimsIdentity GetIdentity(string login)
		{
			var user = new {Login = "", Role = new {Name = ""}}; // get user by login from db
			var claims = new List<Claim>
			{
				new Claim("Name", user.Login)
			};
			if (user.Role != null)
				claims.Add(new Claim("Role", user.Role.Name));

			var claimsIdentity =
				new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
					ClaimsIdentity.DefaultRoleClaimType);
			return claimsIdentity;
		}
	}
}