using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Platform.Web.Controllers
{
	[Route("[controller]/[action]")]
	public class SystemController : Controller
	{
		[HttpGet]
		public IActionResult Error()
		{
			var error = HttpContext
				.Features
				.Get<IExceptionHandlerPathFeature>();
			return Conflict($"Error occured on request {error.Path} : {error.Error.Message}");
		}
	}
}