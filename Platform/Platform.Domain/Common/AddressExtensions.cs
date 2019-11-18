using System;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;

namespace Platform.Domain.Common
{
	internal static class AddressExtensions
	{
		internal static IAddressElement CreateAddressElement(AddressItem elType)
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

		internal static IAddressElement UpdateName(this IAddressElement el, string name)
		{
			el.Name = name;
			return el;
		}

		internal static IAddressElement UpdateParent(this IAddressElement el, string parentPropertyName, int parentId)
		{
			el.GetType().GetProperty(parentPropertyName)?.SetValue(el, parentId);
			return el;
		}
	}
}