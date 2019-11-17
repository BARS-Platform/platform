namespace Platform.Fodels.Models.Address

open System
open Platform.Fodels.Interfaces

type [<AllowNullLiteral>] State() =
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
    val mutable private country: Country
    member this.Country
        with get () = this.country
        and set (country) =
            if not (isNull country) then
                this.country <- country
            else
                raise (new ArgumentNullException(nameof (country)))
