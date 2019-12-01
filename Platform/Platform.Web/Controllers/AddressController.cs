using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Platform.Domain.Common;
using Platform.Domain.DomainServices;
using Platform.Fatabase;
using Platform.Fodels.Enums;
using Platform.Fodels.Models;
using Platform.Fodels.Models.Address;
using Platform.Services.Dto;
using Platform.Services.Common;

namespace Platform.Web.Controllers
{
	[Route("api/[controller]/[action]")]
	public class AddressController : Controller
	{
		private readonly AddressDomainService _service;
        private readonly IRepository _repository;

		public AddressController(
            AddressDomainService service,
            IRepository repository)
		{
			_service = service;
            _repository = repository;
        }

		/// <summary>
		/// Add address element
		/// </summary>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult AddItem(Fodels.Models.AddressDto dto)
		{
			OperationResult Add() => _service.CreateItem(dto);
			return HandleRequest(Add);
		}

		/// <summary>
		/// Delete address element
		/// </summary>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult DeleteItem([Required] AddressItem elType, [Required] int elementId)
		{
			OperationResult Delete() => _service.RemoveItem(elType, elementId);
			return HandleRequest(Delete);
		}

		/// <summary>
		/// Get address element
		/// </summary>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult GetItem([Required] AddressItem elType, [Required] int elementId)
		{
			OperationResult Get() => _service.GetItem(elType, elementId);
			return HandleRequest(Get);
		}

		/// <summary>
		/// Update address element
		/// </summary>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult UpdateItem([Required] AddressItem elType, [Required] int elementId, string newName,
			int parentId)
		{
			if (string.IsNullOrEmpty(newName) && parentId == 0)
				return Ok("No changed data.");

			OperationResult Update() => _service.UpdateItem(elType, elementId, newName, parentId);
			return HandleRequest(Update);
		}

		private IActionResult HandleRequest(Func<OperationResult> function)
		{
			var result = function();
			return result.Success ? (IActionResult) Ok(result) : Conflict(result);
		}

        /// <summary>
        /// Update address element
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var list = _repository.GetAll<Apartment>()
                .Select(x => new Platform.Services.Dto.AddressDto
                {
                    Id = x.Id,
                    CountryName = x.House.Street.City.State.Country.Name,
                    StateName = x.House.Street.City.State.Name,
                    CityName = x.House.Street.City.Name,
                    StreetName = x.House.Street.Name,
                    HouseNumber = x.House.Name,
                    ApartmentNumber = x.Name
                })
                .ToList();

            return Ok(list);
        }
    }
}