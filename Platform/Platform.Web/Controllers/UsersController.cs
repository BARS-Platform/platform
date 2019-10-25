using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platform.Domain.DomainServices;

namespace Platform.Web.Controllers
{
	[AllowAnonymous]
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
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		public IActionResult Register(string login, string password, string email)
		{
            var result = _userDomainService.Register(login, password, email);

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