using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platform.Database;
using Platform.Models;
using Platform.Web.Services;

namespace Platform.Web.Controllers
{
	[AllowAnonymous]
	[Route("api/[controller]/[action]")]
	public class UsersController : Controller
	{
		public UsersController(IRepository<User> repository,
			PasswordService passwordService, TokenService tokenService)
		{
			_repository = repository;
			_passwordService = passwordService;
			_tokenService = tokenService;
		}

		private readonly IRepository<User> _repository;
		private readonly PasswordService _passwordService;
		private readonly TokenService _tokenService;

		/// <summary>
		/// Used to log in and receive new JWT.
		/// </summary>
		/// <returns>Json with 1 property: "token" : *token*</returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public IActionResult LogIn(string login, string password)
		{
			var user = _repository.FindByPredicate(x => x.Login == login);

			if (user == null)
			{
				return Unauthorized("Invalid login. Please check your credentials.");
			}

			if (!_passwordService.Check(user.Password, password))
			{
				return Unauthorized("Invalid password. Please check your credentials.");
			}

			var token = _tokenService.GenerateToken(login);
			return Ok(new
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token)
			});
		}

		/// <summary>
		/// Used to register new users.
		/// </summary>
		/// <returns>Json with 1 property: "token" : *token*</returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		public IActionResult Register(string login, string password, string email)
		{
			var existedUser = _repository.FindByPredicate(x =>x.Login == login);
			if (existedUser != null)
				return Conflict("User with this login already exists. Please, try another one.");
			
			_repository.Create(new User(login, _passwordService.HashPassword(password), email));
			var jwtSecurityToken = _tokenService.GenerateToken(login);

			return Ok(new
			{
				Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
			});
		}
	}
}