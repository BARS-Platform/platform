using Microsoft.AspNetCore.Mvc;
using Platform.Domain.DomainServices;
using Platform.Fatabase;
<<<<<<< HEAD
using Platform.Services.Common;
using Platform.Web.Controllers.Base;
=======
using Platform.Fodels.Models.Address;
using Platform.Services.Common;
using Platform.Services.Dto.AddressDtos;
using Platform.Services.Helpers;
>>>>>>> Реализована пагинация на сервере

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
<<<<<<< HEAD
        [HttpPost]
        public IActionResult GetAll([FromBody] ListParam listParam) =>
            HandleRequest(() => _domainService.GetAllApartments(listParam));
=======
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll(ListParam listParam)
        {
            var list = _repository.GetAll<Apartment>()
                .IncludeAll()
                .Select(ApartmentDto.ProjectionExpression)
                .FormData(listParam);

            return Ok(list);
        }
>>>>>>> Реализована пагинация на сервере
    }
}