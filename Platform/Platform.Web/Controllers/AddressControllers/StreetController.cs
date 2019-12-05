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
    public class StreetController : BaseController<Street>
    {
        public StreetController(IRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// Получить все Улицы.
        /// </summary>
        public override IActionResult GetAll([FromBody] ListParam listParam)
        {
            var list = Repository.GetAll<Street>()
                .IncludeAll()
                .Select(StreetDto.ProjectionExpression)
                .FormData(listParam);

            return Ok(list);
        }
    }
}