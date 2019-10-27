using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Platform.Web.Controllers
{
	[Route("[controller]/[action]")]
	public class SystemController : Controller
	{
		[AcceptVerbs("GET", "POST", "PUT")]
		public IActionResult Error()
		{
			var error = HttpContext
				.Features
				.Get<IExceptionHandlerPathFeature>();
			return Conflict(new
			{
				Message = error.Error.Message,
				RequestPath = error.Path
			});
		}
	}
}