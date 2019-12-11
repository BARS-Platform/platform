using Microsoft.AspNetCore.Mvc;
using Platform.Domain.DomainServices;
using Platform.Fatabase;
using Platform.Services.Common;
using Platform.Web.Controllers.Base;

namespace Platform.Web.Controllers.AddressControllers
{
    [Route("api/[controller]/[action]")]
    public class HouseController : BaseController
    {
        private readonly AddressDomainService _domainService;
        
        public HouseController(IRepository repository, AddressDomainService domainService) : base(repository) =>
            _domainService = domainService;

        /// <summary>
        /// Получить все Дома.
        /// </summary>
        [HttpPost]
        public IActionResult GetAll([FromBody] ListParam listParam) =>
            HandleRequest(() => _domainService.GetAllHouses(listParam));

        [HttpDelete]
        public IActionResult Delete(int entryId) =>
            HandleRequest(() => _domainService.RemoveItem(Fodels.Enums.AddressItem.House, entryId));
    }
}