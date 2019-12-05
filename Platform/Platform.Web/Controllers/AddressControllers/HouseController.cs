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
    public class HouseController : BaseController<House>
    {
        public HouseController(IRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// Получить все Дома.
        /// </summary>
        public override IActionResult GetAll([FromBody] ListParam listParam)
        {
            var list = Repository.GetAll<House>()
                .IncludeAll()
                .Select(HouseDto.ProjectionExpression)
                .FormData(listParam);

            return Ok(list);
        }
    }
}