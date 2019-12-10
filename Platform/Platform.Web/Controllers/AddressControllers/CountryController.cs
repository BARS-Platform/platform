using Microsoft.AspNetCore.Mvc;
using Platform.Domain.DomainServices;
using Platform.Fatabase;
using Platform.Fodels.Enums;
using Platform.Fodels.Models;
using Platform.Fodels.Models.Address;
using Platform.Services.Common;
using Platform.Services.Dto.AddressDtos;
using Platform.Web.Controllers.Base;

namespace Platform.Web.Controllers.AddressControllers
{
    [Route("api/[controller]/[action]")]
    public class CountryController : BaseController
    {
        private readonly AddressDomainService _domainService;
        
        public CountryController(IRepository repository, AddressDomainService domainService) : base(repository) =>
            _domainService = domainService;

        /// <summary>
        /// Получить все Страны.
        /// </summary>
        [HttpPost]
        public IActionResult GetAll([FromBody] ListParam listParam) =>
            HandleRequest(() => _domainService.GetAllCountries(listParam));

        [HttpPost]
        public IActionResult Create([FromBody] CountryDto dto) =>
            HandleRequest(() => _domainService.CreateItem(new AddressDto
            {
                AddressItem = AddressItem.Country,
                Name = dto.CountryName,
                ParentId = 0
            }));

        [HttpDelete]
        public IActionResult Delete(int entryId) =>
            HandleRequest(() => _domainService.RemoveItem(Fodels.Enums.AddressItem.Country, entryId));
    }
}
