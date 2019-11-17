namespace Platform.Fodels.Models.Address

open System
open Platform.Fodels.Interfaces

type [<AllowNullLiteral>] Apartment() =
    interface IEntityBase with
        member this.Id
            with get () = this.id
            and set (value) = this.id <- value

    [<DefaultValue>]
    val mutable private id: int

    [<DefaultValue>]
    val mutable private name: string
    member this.Name
        with get () = this.name
        and set (name) =
            if not (isNull name) then
                this.name <- name
            else
                raise (new ArgumentNullException(nameof (name)))

    [<DefaultValue>]
    val mutable private house: House
    member this.House
        with get () = this.house
        and set (house) =
            if not (isNull house) then
                this.house <- house
            else
                raise (new ArgumentNullException(nameof (house)))
