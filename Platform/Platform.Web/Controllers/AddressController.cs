using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platform.Domain.Common;
using Platform.Domain.DomainServices;
using Platform.Fodels.Enums;
using Platform.Fodels.Models;
using Platform.Fodels.Models.Address;
using Platform.Services.Common;

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
	}
}