namespace Platform.Fodels.Models.Address

open Platform.Fodels.Enums
open System
open Platform.Fodels.Interfaces
open Platform.Fodels.Attributes

type [<AllowNullLiteral>] [<MenuAttribute("Address", PermissionNamesForFodels.ViewModels, Sections.Models, "Address", Icons.Home)>] Address() =
    member this.Id
        with get () = this.id
        and set (value) = this.id <- value

    interface IEntityBase with
        member this.Id
            with get () = this.Id
            and set (value) = this.Id <- value
    
    [<DefaultValue>]
    val mutable private id: int

    [<DefaultValue>]
    val mutable private apartment: Apartment
    member this.Apartment
        with get () = this.apartment
        and set (apartment) =
            if not (isNull apartment) then
                this.apartment <- apartment
            else
                raise (new ArgumentNullException(nameof (apartment)))
    
    [<DefaultValue>]
    val mutable private floor: int
    
    member this.Floor
        with get () = this.floor
        and set (value) = this.floor <- value
        
    [<DefaultValue>]
    val mutable private index: string
    
    member this.Index
        with get () = this.index
        and set (value) = this.index <- value
        
    [<DefaultValue>]
    val mutable private entranceNumber: int
    
    member this.EntranceNumber
        with get () = this.entranceNumber
        and set (value) = this.entranceNumber <- value