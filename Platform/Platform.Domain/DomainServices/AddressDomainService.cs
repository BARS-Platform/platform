using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Platform.Fatabase;
using Platform.Fodels.Enums;
using Platform.Fodels.Models;
using Platform.Fodels.Models.Address;
using Platform.Services.Common;
using Platform.Services.Dto.AddressDtos;
using Platform.Services.Helpers;

namespace Platform.Domain.DomainServices
{
	public class AddressDomainService
	{
		private readonly IRepository _repository;
		private readonly Type[] _addressTypes;

		public AddressDomainService(IRepository repository)
		{
			_repository = repository;
			_addressTypes = TypeHelper.GetTypes(typeof(IAddressElement));
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
		
		/// <summary>
		/// Получить все Страны.
		/// </summary>
		public OperationResult GetAllCountries(ListParam listParam)
		{
			var list = _repository.GetAll<Country>()
				.Select(CountryDto.ProjectionExpression)
				.FormData(listParam);

			return GenerateAddressResult(list);
		}

		public OperationResult CreateCountry(CountryDto dto)
		{
			var country = new Country
			{
				Name = dto.CountryName
			};
			var result = _repository.Create(country);

			return result == null
				? new OperationResult(false, "Country create error")
				: new OperationResult(true, result);
		}
		
		/// <summary>
		/// Получить все Регионы.
		/// </summary>
		public OperationResult GetAllStates(ListParam listParam)
		{
			var list = _repository.GetAll<State>()
				.Select(StateDto.ProjectionExpression)
				.FormData(listParam);

			return GenerateAddressResult(list);
		}
		
		/// <summary>
		/// Получить все Города.
		/// </summary>
		public OperationResult GetAllCities(ListParam listParam)
		{
			var list = _repository.GetAll<City>()
				.Select(CityDto.ProjectionExpression)
				.FormData(listParam);

			return GenerateAddressResult(list);
		}
		
		/// <summary>
		/// Получить все Улицы.
		/// </summary>
		public OperationResult GetAllStreets(ListParam listParam)
		{
			var list = _repository.GetAll<Street>()
				.Select(StreetDto.ProjectionExpression)
				.FormData(listParam);

			return GenerateAddressResult(list);
		}
		
		/// <summary>
		/// Получить все Дома.
		/// </summary>
		public OperationResult GetAllHouses(ListParam listParam)
		{
			var list = _repository.GetAll<House>()
				.Select(HouseDto.ProjectionExpression)
				.FormData(listParam);

			return GenerateAddressResult(list);
		}
		
		/// <summary>
		/// Получить все Квартиры.
		/// </summary>
		public OperationResult GetAllApartments(ListParam listParam)
		{
			var list = _repository.GetAll<Apartment>()
				.Select(ApartmentDto.ProjectionExpression)
				.FormData(listParam);

			return GenerateAddressResult(list);
		}

		private IAddressElement DynamicGetFromRepository(string typeName, int elementId)
		{
			var elType = _addressTypes.FirstOrDefault(x => x.Name == typeName);
			if (elType == null)
				return null;
			return (IAddressElement) typeof(IRepository).GetMethod("Get")?.MakeGenericMethod(elType)
				.Invoke(_repository, new[] {(object) elementId});
		}

		private IAddressElement GetAddressElement(AddressItem elType, int elementId)
		{
			return elType switch
			{
				AddressItem.Country => (IAddressElement) _repository.Get<Country>(elementId),
				AddressItem.State => _repository.GetWithRelated<State>(elementId, s => s.Include(x => x.Country)),
				AddressItem.City => _repository.GetWithRelated<City>(elementId, queryable => queryable.IncludeAllAddressItems()),
				AddressItem.Street => _repository.GetWithRelated<Street>(elementId, queryable => queryable.IncludeAllAddressItems()),
				AddressItem.House => _repository.GetWithRelated<House>(elementId, queryable => queryable.IncludeAllAddressItems()),
				AddressItem.Apartment => _repository.GetWithRelated<Apartment>(elementId, queryable => queryable.IncludeAllAddressItems()),
				_ => null
			};
		}

		private OperationResult GenerateAddressResult<T>(ListResult<T> list)
		{
			return list == null
				? new OperationResult(false, $"No {nameof(T)} found")
				: new OperationResult(true, list);
		}
	}
}
