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
    public class StreetController : BaseController
    {
        private readonly AddressDomainService _domainService;
        
        public StreetController(IRepository repository, AddressDomainService domainService) : base(repository) =>
            _domainService = domainService;

        /// <summary>
        /// Получить все Улицы.
        /// </summary>
        [HttpPost]
        public IActionResult GetAll([FromBody] ListParam listParam) =>
            HandleRequest(() => _domainService.GetAllStreets(listParam));
        
        [HttpPost]
        public IActionResult Create([FromBody] StreetDto dto) =>
            HandleRequest(() => _domainService.CreateItem(new AddressDto
            {
                AddressItem = AddressItem.Street,
                Name = dto.StreetName,
                ParentId = dto.CityId
            }));
        
        [HttpPost]
        public IActionResult Update([FromBody] StreetDto dto) =>
            HandleRequest(() => _domainService
                .UpdateItem(AddressItem.Street, dto.Id, dto.StreetName, dto.CityId));

        [HttpDelete]
        public IActionResult Delete(int entryId) =>
            HandleRequest(() => _domainService.RemoveItem(Fodels.Enums.AddressItem.Street, entryId));
    }
}
