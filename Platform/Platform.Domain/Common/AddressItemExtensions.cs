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
				AddressItem.City => AddressItem.State.ToString(),
				AddressItem.Street => AddressItem.City.ToString(),
				AddressItem.House => AddressItem.Street.ToString(),
				AddressItem.Apartment => AddressItem.House.ToString(),
				_ => null
			};
		}
	}
}