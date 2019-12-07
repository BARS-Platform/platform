using Microsoft.AspNetCore.Mvc;
using Platform.Domain.DomainServices;
using Platform.Fatabase;
using Platform.Services.Common;
using Platform.Web.Controllers.Base;

namespace Platform.Web.Controllers.AddressControllers
{
    [Route("api/[controller]/[action]")]
    public class CityController : BaseController
    {
        private readonly AddressDomainService _domainService;
        
        public CityController(IRepository repository, AddressDomainService domainService) : base(repository) =>
            _domainService = domainService;

        /// <summary>
        /// Получить все Города.
        /// </summary>
        [HttpPost]
        public IActionResult GetAll([FromBody] ListParam listParam) =>
            HandleRequest(() => _domainService.GetAllCities(listParam));
    }
}