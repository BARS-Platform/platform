using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Platform.Web
{
	[Route("[controller]/[action]" )]
	public class TestController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return Ok("It's d4n0n_myself!");
		}
	}
}