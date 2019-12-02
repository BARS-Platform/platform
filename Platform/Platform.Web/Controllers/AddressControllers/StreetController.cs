using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platform.Fatabase;
using Platform.Fodels.Models.Address;
using Platform.Services.Dto.AddressDtos;
using Platform.Services.Helpers;

namespace Platform.Web.Controllers.AddressControllers
{
    [Route("api/[controller]/[action]")]
    public class StreetController : Controller
    {
        private readonly IRepository _repository;

        public StreetController(IRepository repository) => _repository = repository;

        /// <summary>
        /// Получить все Адреса.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var list = _repository.GetAll<Street>()
                .IncludeAll()
                .Select(StreetDto.ProjectionExpression)
                .ToList();

            return Ok(list);
        }
    }
}