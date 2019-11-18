namespace Platform.Fodels.Models

open Platform.Fodels.Enums

type AddressDto() =
    member val AddressItem = AddressItem.Country: AddressItem
    member val Name = "": string
    member val ParentId = 0: int
