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
        
        [HttpPost]
        public IActionResult Create([FromBody] CityDto dto) =>
            HandleRequest(() => _domainService.CreateItem(new AddressDto
            {
                AddressItem = AddressItem.City,
                Name = dto.CityName,
                ParentId = Repository.FindByPredicate<State>(x => x.Name == dto.StateName)?.Id ?? 0
            }));

        [HttpDelete]
        public IActionResult Delete(int entryId) =>
            HandleRequest(() => _domainService.RemoveItem(Fodels.Enums.AddressItem.City, entryId));
    }
}
