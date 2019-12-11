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
        
        [HttpPost]
        public IActionResult Create([FromBody] StateDto dto) =>
            HandleRequest(() => _domainService.CreateItem(new AddressDto
            {
                AddressItem = AddressItem.State,
                Name = dto.StateName,
                ParentId = dto.CountryId
            }));
        
        [HttpPost]
        public IActionResult Update([FromBody] StateDto dto) =>
            HandleRequest(() => _domainService
                .UpdateItem(AddressItem.State, dto.Id, dto.StateName, dto.CountryId));

        [HttpDelete]
        public IActionResult Delete(int entryId) =>
            HandleRequest(() => _domainService.RemoveItem(Fodels.Enums.AddressItem.State, entryId));


    }
}
