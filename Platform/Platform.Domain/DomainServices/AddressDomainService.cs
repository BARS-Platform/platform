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

		public AddressDomainService(IRepository repository)
		{
			_repository = repository;
		}

		public OperationResult CreateItem(AddressDto dto)
		{
			var addressElement = AddressExtensions.CreateAddressElement(dto.AddressItem)
				.UpdateName(dto.Name)
				.UpdateParent(dto.AddressItem.GetParentPropertyName(), dto.ParentId);
			var element = _repository.Create(addressElement);
			return element == null
				? new OperationResult(false, "Failed to create element.")
				: new OperationResult(true, element);
		}

		public OperationResult GetItem(AddressItem elType, int elementId)
		{
			var res = elType switch
			{
				AddressItem.Country => _repository.Get<Country>(elementId),
				AddressItem.State => _repository.Get<Country>(elementId),
				AddressItem.City => _repository.Get<Country>(elementId),
				AddressItem.Street => _repository.Get<Country>(elementId),
				AddressItem.House => _repository.Get<Country>(elementId),
				AddressItem.Apartment => _repository.Get<Country>(elementId),
				_ => null
			};
			return res == null 
				? new OperationResult(false, "Element was not found.") 
				: new OperationResult(true, res);
		}

		public OperationResult RemoveItem(IAddressElement el) =>
			_repository.Delete(el) 
				? new OperationResult(true) 
				: new OperationResult(false);

		public OperationResult UpdateItem(IAddressElement el)
		{
			var res = _repository.Update(el);
			return new OperationResult(true, res);
		}
	}
}