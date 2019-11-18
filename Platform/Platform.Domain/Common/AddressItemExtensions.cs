using Platform.Fodels.Enums;

namespace Platform.Domain.Common
{
	public static class AddressItemExtensions
	{
		public static string GetParentPropertyName(this AddressItem elType)
		{
			return elType switch
			{
				AddressItem.State => AddressItem.Country.ToString(),
				AddressItem.City => AddressItem.Country.ToString(),
				AddressItem.Street => AddressItem.Country.ToString(),
				AddressItem.House => AddressItem.Country.ToString(),
				AddressItem.Apartment => AddressItem.Country.ToString(),
				_ => null
			};
		}
	}
}