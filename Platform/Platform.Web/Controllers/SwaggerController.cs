using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Platform.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SwaggerController : Controller
    {
        /// <summary>
        /// Метод для получения модели из схемы Swagger.
        /// </summary>
        /// <param name="modelName">Наименование модели в схеме.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> FormModelFromSchema(string modelName)
        {
            var httpClient = new HttpClient();
            var schema = await httpClient
                .GetStringAsync($@"http://{HttpContext.Request.Host}/swagger/{Startup.SwaggerConfigurationName}/swagger.json");

            var jModel = JObject.Parse(schema)?["components"]?["schemas"]?[modelName];

            if (jModel == null)
            {
                return Conflict("Не найдена соответствующая модель в схеме");
            }

            return Ok(jModel.ToString());
        }
    }
}
