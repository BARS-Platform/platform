using Microsoft.AspNetCore.Mvc;
using Platform.Domain.DomainServices;
using Platform.Fatabase;
using Platform.Services.Common;
using Platform.Web.Controllers.Base;

namespace Platform.Web.Controllers.AddressControllers
{
    [Route("api/[controller]/[action]")]
    public class StateController : BaseController
    {
        private readonly AddressDomainService _domainService;
        
        public StateController(IRepository repository, AddressDomainService domainService) : base(repository) =>
            _domainService = domainService;

        /// <summary>
        /// Получить все Регионы.
        /// </summary>
        [HttpPost]
        public IActionResult GetAll([FromBody] ListParam listParam) =>
            HandleRequest(() => _domainService.GetAllStates(listParam));

        [HttpDelete]
        public IActionResult Delete(int entryId) =>
            HandleRequest(() => _domainService.RemoveItem(Fodels.Enums.AddressItem.State, entryId));


    }
}