namespace Platform.Fodels.Models.Address

open Platform.Fodels.Enums
open System
open Platform.Fodels.Interfaces
open Platform.Fodels.Attributes

type [<AllowNullLiteral>] [<MenuAttribute("Город", PermissionNamesForFodels.ViewModels, Sections.Models, "City", Icons.Star)>] City() =
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
        member this.Type = AddressItem.City

    [<DefaultValue>]
    val mutable private id: int

    [<DefaultValue>]
    val mutable private name: string

    [<DefaultValue>]
    val mutable private state: State
    member this.State
        with get () = this.state
        and set (state) =
            if not (isNull state) then
                this.state <- state
            else
                raise (new ArgumentNullException(nameof (state)))
