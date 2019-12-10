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
        
        [HttpPost]
        public IActionResult Create([FromBody] HouseDto dto) =>
            HandleRequest(() => _domainService.CreateItem(new AddressDto
            {
                AddressItem = AddressItem.House,
                Name = dto.HouseNumber,
                ParentId = Repository.FindByPredicate<Street>(x => x.Name == dto.StreetName)?.Id ?? 0
            }));

        [HttpDelete]
        public IActionResult Delete(int entryId) =>
            HandleRequest(() => _domainService.RemoveItem(Fodels.Enums.AddressItem.House, entryId));
    }
}
