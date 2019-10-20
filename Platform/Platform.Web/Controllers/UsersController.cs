using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Platform.Database;
using Platform.Models;

namespace Platform.Web.Controllers
{
	[Route("[controller]/[action]")]
	public class UsersController : Controller
	{
		public IRepository<User> Repository { get; set; }
		
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

			//todo 
//			var repository = new Repository<User>(new ApplicationDbContext());

			Repository.Create(new User() {Login = login, Password = HashPassword(password)});
			var jwtSecurityToken = GenerateToken(login);
			
			return Ok(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
		}

		[HttpGet]
		public IActionResult GetToken()
		{
			return Ok(new
			{
				Token = new JwtSecurityTokenHandler().WriteToken(GenerateToken(string.Empty))
			});
		}

		private JwtSecurityToken GenerateToken(string login)
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

		private string HashPassword(string password)
		{
			// generate a 128-bit salt using a secure PRNG
			var salt = new byte[128 / 8];
			using (var rng = RandomNumberGenerator.Create())
				rng.GetBytes(salt);

			// derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
			var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password,
				salt,
				KeyDerivationPrf.HMACSHA1,
				10000,
				256 / 8));
			return hashed;
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