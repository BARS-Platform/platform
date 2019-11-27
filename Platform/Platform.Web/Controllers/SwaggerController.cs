using System.Linq;
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
        [HttpGet]
        public async Task<IActionResult> FormModelFromSchema(string modelName)
        {
            var httpClient = new HttpClient();
            var swaggerSchema = await httpClient
                .GetStringAsync($@"http://{HttpContext.Request.Host}/swagger/{Startup.SwaggerConfigurationName}/swagger.json");

            var modelSchemas = JObject.Parse(swaggerSchema)["components"]["schemas"];

            var jModel = modelSchemas[modelName];

            if (jModel == null)
            {
                return Conflict("Не найдена соответствующая модель в схеме");
            }

            var refProperties = jModel["properties"]
                .Values()
                .Where(x => x.ToString().StartsWith("{\r\n  \"$ref\""))
                .ToList();

            foreach(var item in refProperties)
            {
                var refModelName = item.ToString().Split('/').Last().Split(@"""").First();
                jModel["properties"][GetCorrectName(refModelName)] = modelSchemas[refModelName] ;
            }

            return Ok(jModel.ToString());
        }

        private string GetCorrectName(string name)
        {
            return name.First().ToString().ToLower() + name.Substring(1);
        }
    }
}
