using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platform.Domain.Common;
using Platform.Domain.DomainServices;
using Platform.Fodels.Enums;
using Platform.Fodels.Models;
using Platform.Fodels.Models.Address;

namespace Platform.Web.Controllers
{
	[Route("api/[controller]/[action]")]
	public class AddressController : Controller
	{
		private readonly AddressDomainService _service;

		public AddressController(AddressDomainService service)
		{
			_service = service;
		}

		/// <summary>
		/// Add address element
		/// </summary>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult AddItem(AddressDto dto)
		{
			OperationResult Add()
			{
				return _service.CreateItem(dto);
			}

			return HandleRequest(Add);
		}

		/// <summary>
		/// Delete address element
		/// </summary>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult DeleteItem(IAddressElement element)
		{
			OperationResult Delete()
			{
				return _service.RemoveItem(element);
			}

			return HandleRequest(Delete);
		}

		/// <summary>
		/// Get address element
		/// </summary>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult GetItem([Required] AddressItem elType, [Required] int id)
		{
			OperationResult Get()
			{
				return _service.GetItem(elType, id);
			}

			return HandleRequest(Get);
		}

		/// <summary>
		/// Update address element
		/// </summary>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult UpdateItem(IAddressElement element)
		{
			OperationResult Update()
			{
				return _service.UpdateItem(element);
			}

			return HandleRequest(Update);
		}

		private IActionResult HandleRequest(Func<OperationResult> function)
		{
			var result = function();
			return result.Success ? (IActionResult) Ok(result) : Conflict(result);
		}
	}
}