using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Platform.Web.Controllers
{
	[Authorize("PlatformUser")]
	[Route("api/[controller]/[action]")]
	public class MetadataController : Controller
	{
		[HttpGet]
		[Obsolete]
		public IActionResult GetMetadata()
		{
			// Метод использовался для тестирования системы аутентификации с помощью JWT токенов.
			// TODO: удалить после появления контроллеров, пригодных для подобных тестов.
			return Ok(new {Result = "!"});
		}
	}
}