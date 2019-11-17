namespace Platform.Fodels.Models.Address

open System
open Platform.Fodels.Interfaces

type [<AllowNullLiteral>] City() =
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
    val mutable private state: State
    member this.State
        with get () = this.state
        and set (state) =
            if not (isNull state) then
                this.state <- state
            else
                raise (new ArgumentNullException(nameof (state)))
