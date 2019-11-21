namespace Platform.Fodels.Models.Address

open Platform.Fodels.Enums
open System
open Platform.Fodels.Interfaces
open Platform.Fodels.Attributes

type [<AllowNullLiteral>] [<MenuAttribute("House", "ViewModels", "Модели", "house", "home")>] House() =
    member this.Id
        with get () = this.id
        and set (value) = this.id <- value

    interface IEntityBase with
        member this.Id
            with get () = this.Id
            and set (value) = this.Id <- value

    member this.Name
        with get () = this.name
            and set (name) =
                if not (isNull name) then
                    this.name <- name
                else
                    raise (new ArgumentNullException(nameof (name)))

    interface IAddressElement with
        member this.Name
            with get () = this.Name
            and set (value) = this.Name <- value
        member this.Type = AddressItem.House

    [<DefaultValue>]
    val mutable private id: int

    [<DefaultValue>]
    val mutable private name: string

    [<DefaultValue>]
    val mutable private street: Street
    member this.Street
        with get () = this.street
        and set (street) =
            if not (isNull street) then
                this.street <- street
            else
                raise (new ArgumentNullException(nameof (street)))
