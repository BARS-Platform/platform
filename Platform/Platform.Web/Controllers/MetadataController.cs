using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Platform.Web.Controllers
{
	[Authorize]
	[Route("[controller]/[action]")]
	public class MetadataController : Controller
	{
		[HttpGet]
		public IActionResult GetMetadata()
		{
			// Метод использовался для тестирования системы аутентификации с помощью JWT токенов.
			// TODO: по выполнению требований задач избавиться от данного метода/контроллера.
			return Ok(new {Result = "!"});
		}
	}
}