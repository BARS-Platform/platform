namespace Platform.Fodels.Models.Address

open System
open Platform.Fodels.Interfaces

type [<AllowNullLiteral>] Street() =
    interface IEntityBase with
        member this.Id
            with get () = this.id
            and set (value) = this.id <- value

    interface IAddressElement with
        member this.Name
            with get () = this.name
            and set (name) =
                if not (isNull name) then
                    this.name <- name
                else
                    raise (new ArgumentNullException(nameof (name)))

    [<DefaultValue>]
    val mutable private id: int

    [<DefaultValue>]
    val mutable private name: string

    [<DefaultValue>]
    val mutable private city: City
    member this.City
        with get () = this.city
        and set (city) =
            if not (isNull city) then
                this.city <- city
            else
                raise (new ArgumentNullException(nameof (city)))
