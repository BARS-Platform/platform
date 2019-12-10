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
    public class ApartmentController : BaseController
    {
        private readonly AddressDomainService _domainService;

        public ApartmentController(IRepository repository, AddressDomainService domainService) : base(repository) =>
            _domainService = domainService;

        /// <summary>
        /// Получить все Квартиры.
        /// </summary>
        [HttpPost]
        public IActionResult GetAll([FromBody] ListParam listParam) =>
            HandleRequest(() => _domainService.GetAllApartments(listParam));

        [HttpPost]
        public IActionResult Create([FromBody] ApartmentDto dto) =>
            HandleRequest(() => _domainService.CreateItem(new AddressDto
            {
                AddressItem = AddressItem.Apartment,
                Name = dto.ApartmentNumber.ToString(),
                ParentId = Repository.FindByPredicate<House>(x => x.Name == dto.HouseNumber)?.Id ?? 0
            }));

        [HttpDelete]
        public IActionResult Delete(int entryId) =>
            HandleRequest(() => _domainService.RemoveItem(Fodels.Enums.AddressItem.Apartment, entryId));
    }
}
