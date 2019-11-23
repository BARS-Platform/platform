using System;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;

namespace Platform.Services.Helpers
{
	public static class AddressExtensions
	{
		public static IAddressElement CreateAddressElement(AddressItem elType)
		{
			return elType switch
			{
				AddressItem.Country => (IAddressElement) new Country(),
				AddressItem.State => new State(),
				AddressItem.City => new City(),
				AddressItem.Street => new Street(),
				AddressItem.House => new House(),
				AddressItem.Apartment => new Apartment(),
				_ => throw new ArgumentException(
					"Can't find matching address type. Try reviewing your input parameters.")
			};
		}

		public static IAddressElement UpdateName(this IAddressElement el, string name)
		{
			if (!string.IsNullOrEmpty(name))
				el.Name = name;
			return el;
		}

		public static IAddressElement UpdateParent(this IAddressElement el, string parentPropertyName,
			object parentId)
		{
			if (parentPropertyName == null || parentId == null)
				return el;
			el.GetType().GetProperty(parentPropertyName)?.SetValue(el, parentId);
			return el;
		}
	}
}