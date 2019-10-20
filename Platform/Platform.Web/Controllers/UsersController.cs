using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Platform.Database;
using Platform.Models;
using Platform.Web.Services;

namespace Platform.Web.Controllers
{
	[Route("[controller]/[action]")]
	public class UsersController : Controller
	{
		public UsersController(IRepository<User> repository,
			PasswordCheckerService checkerService, TokenService tokenService)
		{
			_repository = repository;
			_checkerService = checkerService;
			_tokenService = tokenService;
		}

		private readonly IRepository<User> _repository;
		private readonly PasswordCheckerService _checkerService;
		private readonly TokenService _tokenService;

		/// <summary>
		/// Used to log in and receive new JWT.
		/// </summary>
		/// <returns>Json with 1 property: "token" : *token*</returns>
		[HttpGet]
		public IActionResult LogIn(string login, string password)
		{
			var user = _repository.FindByPredicate(x => x.Login == login);

			if (user == null)
			{
				return Unauthorized("Invalid login. Please check your credentials.");
			}

			if (!_checkerService.Check(user.Password, password))
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
		public IActionResult Register(string login, string password)
		{
			_repository.Create(new User() {Login = login, Password = _checkerService.HashPassword(password)});
			var jwtSecurityToken = _tokenService.GenerateToken(login);

			return Ok(new
			{
				Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
			});
		}
	}
}