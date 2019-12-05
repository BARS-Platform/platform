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
    public class ApartmentController : BaseController<Apartment>
    {
        public ApartmentController(IRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// Получить все Квартиры.
        /// </summary>
        public override IActionResult GetAll([FromBody] ListParam listParam)
        {
            var list = Repository.GetAll<Apartment>()
                .IncludeAll()
                .Select(ApartmentDto.ProjectionExpression)
                .FormData(listParam);

            return Ok(list);
        }
    }
}