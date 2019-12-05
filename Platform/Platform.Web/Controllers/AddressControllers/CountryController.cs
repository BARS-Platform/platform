using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Platform.Fatabase;
using Platform.Fodels.Models.Address;
using Platform.Services.Common;
using Platform.Services.Dto.AddressDtos;
using Platform.Web.Controllers.Base;

namespace Platform.Web.Controllers.AddressControllers
{
    [Route("api/[controller]/[action]")]
    public class CountryController : BaseController<Country>
    {
        public CountryController(IRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// Получить все Страны.
        /// </summary>
        public override IActionResult GetAll([FromBody] ListParam listParam)
        {
            var list = Repository.GetAll<Country>()
                .Select(CountryDto.ProjectionExpression)
                .FormData(listParam);

            return Ok(list);
        }
    }
}