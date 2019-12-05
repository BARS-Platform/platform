using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Platform.Fatabase;
using Platform.Fodels.Models.Address;
using Platform.Services.Common;
using Platform.Services.Dto.AddressDtos;
using Platform.Services.Helpers;
using Platform.Web.Controllers.Base;

namespace Platform.Web.Controllers.AddressControllers
{
    [Route("api/[controller]/[action]")]
    public class CityController : BaseController<City>
    {
        public CityController(IRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// Получить все Города.
        /// </summary>
        public override IActionResult GetAll([FromBody] ListParam listParam)
        {
            var list = Repository.GetAll<City>()
                .IncludeAll()
                .Select(CityDto.ProjectionExpression)
                .FormData(listParam);

            return Ok(list);
        }
    }
}