using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platform.Domain.Common;
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
		/// Used to check, whether this login is used
		/// </summary>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult CheckLoginUsed([Required] string login)
		{
			var operationResult = _userDomainService.CheckLoginUsed(login);
			return operationResult.Success ? (IActionResult) Ok(operationResult) : Conflict(operationResult);
		}

		/// <summary>
		/// Used to check, whether this email is used
		/// </summary>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult CheckEmailUsed([Required] string email)
		{
			var operationResult = _userDomainService.CheckEmailUsed(email);
			return operationResult.Success ? (IActionResult) Ok(operationResult) : Conflict(operationResult);
		}

		/// <summary>
		/// Used to log in and receive new JWT.
		/// </summary>
		/// <returns>Json with 1 property: "token" : *token*</returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public IActionResult LogIn([Required] string login, [Required] string password)
		{
			OperationResult result;
			try
			{
				result = _userDomainService.LogIn(login, password);
			}
			catch (AuthorizationException exception)
			{
				return NotFound(new
				{
					Message = exception.Message,
					ParameterName = exception.ParameterName
				});
			}

			return result.Success ? (IActionResult) Ok(result) : Conflict(result);
		}

		/// <summary>
		/// Used to register new users.
		/// </summary>
		/// <returns>Json with 1 property: "token" : *token*</returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		public IActionResult Register([Required] string login, [Required] string password,
			[Required] string email)
		{
			if (string.IsNullOrEmpty(password))
				return BadRequest("Given password was not valid. Please, consider changing your password.");

			var result = _userDomainService.Register(login, password, email);

			if (result.Success)
			{
				return Ok(result);
			}
			return Conflict(result);
		}
	}
}