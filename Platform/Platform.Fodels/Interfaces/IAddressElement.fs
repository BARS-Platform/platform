namespace Platform.Fodels.Models.Address
open Platform.Fodels.Interfaces

type [<AllowNullLiteral>] IAddressElement =
    inherit IEntityBase
        abstract Name: string with get, set

