using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Platform.Web.Controllers
{
	[Route("[controller]/[action]")]
	public class SystemController : Controller
	{
		private readonly ILogger<SystemController> _logger;

		public SystemController(ILogger<SystemController> logger)
		{
			_logger = logger;
		}

		[AcceptVerbs("GET", "POST", "PUT", "DELETE", "HEAD", "OPTIONS", "PATCH")]
		public IActionResult Error()
		{
			var handlerResult = HttpContext
				.Features
				.Get<IExceptionHandlerPathFeature>();
			_logger.LogError(handlerResult.Error,
				$"Unhandled error on {handlerResult.Path}:{Environment.NewLine} {handlerResult.Error}");
			return Conflict(new
			{
				handlerResult.Error.Message,
				RequestPath = handlerResult.Path
			});
		}
	}
}