using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platform.Fatabase;
using Platform.Fodels.Models.Address;
using Platform.Services.Common;
using Platform.Services.Dto.AddressDtos;

namespace Platform.Web.Controllers.AddressControllers
{
    [Route("api/[controller]/[action]")]
    public class CountryController : Controller
    {
        private readonly IRepository _repository;

        public CountryController(IRepository repository) => _repository = repository;

        /// <summary>
        /// Получить все Адреса.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll([FromBody] ListParam listParam)
        {
            var list = _repository.GetAll<Country>()
                .Select(CountryDto.ProjectionExpression)
                .FormData(listParam);

            return Ok(list);
        }
    }
}