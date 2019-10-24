using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Platform.Database;
using Platform.Domain.DomainServices;
using Platform.Models;
using Platform.Web.Services;

namespace Platform.Web.Controllers
{
	[Route("api/[controller]/[action]")]
	public class UsersController : Controller
	{
        private readonly UserDomainService _userDomainService;

        public UsersController(UserDomainService userDomainService)
		{
            _userDomainService = userDomainService;
        }

		/// <summary>
		/// Used to log in and receive new JWT.
		/// </summary>
		/// <returns>Json with 1 property: "token" : *token*</returns>
		[HttpGet]
		public IActionResult LogIn(string login, string password)
		{
            var result = _userDomainService.LogIn(login, password);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return Conflict(result);
            }
		}

		/// <summary>
		/// Used to register new users.
		/// </summary>
		/// <returns>Json with 1 property: "token" : *token*</returns>
		[HttpPost]
		public IActionResult Register(string login, string password)
		{
            var result = _userDomainService.Register(login, password);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return Conflict(result);
            }
        }
	}
}