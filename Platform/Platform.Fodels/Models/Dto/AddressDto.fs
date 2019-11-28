namespace Platform.Fodels.Models

open Platform.Fodels.Enums

type AddressDto() =

    [<DefaultValue>]
    val mutable private adrItem: AddressItem
    member this.AddressItem
        with get () = this.adrItem
        and set (value) = this.adrItem <- value

    [<DefaultValue>]
    val mutable private name: string
    member this.Name
        with get () = this.name
        and set (value) = this.name <- value

    [<DefaultValue>]
    val mutable private parentId: int
    member this.ParentId
        with get () = this.parentId
        and set (value) = this.parentId <- value
