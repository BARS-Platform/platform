using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.Fodels;
using Platform.Fodels.Models;

namespace Platform.Web.Controllers
{
	[Authorize("PlatformUser")]
	[Route("api/[controller]/[action]")]
	[Obsolete("Будет удалено после создания более корректного тестового материала")]
	public class MetadataController : Controller
	{
		[HttpGet]
		[Obsolete("Будет удалено после создания более корректного тестового материала")]
		public IActionResult GetMetadata()
		{
			// Метод использовался для тестирования системы аутентификации с помощью JWT токенов.
			// TODO: удалить после появления контроллеров, пригодных для подобных тестов.
			return Ok(new {Result = "!"});
		}

		[HttpGet]
		[Obsolete("Будет удалено после создания более корректного тестового материала")]
		public IActionResult GetCurrentEnvironment()
		{
			return Ok(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
		}

		[HttpGet]
		[Obsolete("Будет удалено после создания более корректного тестового материала")]
		public User Test()
		{
			throw new Exception("Super test!11!!!");
		}
	}
}