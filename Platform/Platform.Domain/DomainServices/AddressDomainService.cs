using System;
using System.Linq;
using Platform.Domain.Common;
using Platform.Fatabase;
using Platform.Fodels.Enums;
using Platform.Fodels.Models;
using Platform.Fodels.Models.Address;

namespace Platform.Domain.DomainServices
{
	public class AddressDomainService
	{
		private readonly IRepository _repository;
		private readonly Type[] _addressTypes;

		public AddressDomainService(IRepository repository)
		{
			_repository = repository;
			_addressTypes = AppDomain.CurrentDomain
				.GetAssemblies()
				.Where(x => !x.IsDynamic)
				.SelectMany(x => x.GetExportedTypes()
					.Where(y => y.IsClass)
					.Where(y => typeof(IAddressElement).IsAssignableFrom(y)))
				.ToArray();
		}

		public OperationResult CreateItem(AddressDto dto)
		{
			var parentPropertyName = dto.AddressItem.GetParentPropertyName();
			var parentEl = DynamicGetFromRepository(parentPropertyName, dto.ParentId);

			var addressElement = AddressExtensions.CreateAddressElement(dto.AddressItem)
				.UpdateName(dto.Name)
				.UpdateParent(parentPropertyName, parentEl);

			var element = _repository.Create(addressElement);
			return element == null
				? new OperationResult(false, "Failed to create element.")
				: new OperationResult(true, element);
		}

		public OperationResult GetItem(AddressItem elType, int elementId)
		{
			var res = GetAddressElement(elType, elementId);

			return res == null
				? new OperationResult(false, "Element was not found.")
				: new OperationResult(true, res);
		}

		public OperationResult RemoveItem(AddressItem elType, int elementId)
		{
			var el = GetAddressElement(elType, elementId);

			if (el == null)
				return new OperationResult(false, "Element was not found.");

			var res = _repository.Delete(el);

			return res
				? new OperationResult(true)
				: new OperationResult(false, "Failed to delete element.");
		}

		public OperationResult UpdateItem(AddressItem elType, int elementId, string newName, int parentId)
		{
			var el = DynamicGetFromRepository(elType.ToString(), elementId);
			if (el == null)
				return new OperationResult(false, "Element was not found.");

			var parentPropertyName = elType.GetParentPropertyName();
			var newParent = DynamicGetFromRepository(parentPropertyName, parentId);

			el.UpdateName(newName)
				.UpdateParent(el.Type.GetParentPropertyName(), newParent);
			var res = _repository.Update(el);
			return new OperationResult(true, res);
		}

		private IAddressElement DynamicGetFromRepository(string typeName, int elementId)
		{
			var elType = _addressTypes.FirstOrDefault(x => x.Name == typeName);
			return (IAddressElement) typeof(IRepository).GetMethod("Get")?.MakeGenericMethod(elType)
				.Invoke(_repository, new[] {(object) elementId});
		}

		private IAddressElement GetAddressElement(AddressItem elType, int elementId)
		{
			return elType switch
			{
				AddressItem.Country => (IAddressElement) _repository.Get<Country>(elementId),
				AddressItem.State => _repository.Get<State>(elementId),
				AddressItem.City => _repository.Get<City>(elementId),
				AddressItem.Street => _repository.Get<Street>(elementId),
				AddressItem.House => _repository.Get<House>(elementId),
				AddressItem.Apartment => _repository.Get<Apartment>(elementId),
				_ => null
			};
		}
	}
}