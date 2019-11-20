namespace Platform.Fodels.Models

open Platform.Fodels.Attributes
open Platform.Fodels.Enums
open Platform.Fodels.Interfaces

type [<AllowNullLiteral>] [<Platform(AttributesEnum.Grid ||| AttributesEnum.Form)>] CurrencyReferenceBook() =
    member this.Id
        with get () = this.id
        and set (value) = this.id <- value

    interface IEntityBase with
        member this.Id
            with get () = this.Id
            and set(value) = this.Id <- value

    [<DefaultValue>]
    val mutable private id: int

    [<DefaultValue>]
    val mutable private name: string
    member this.Name
        with get () = this.name
        and set (value) = this.name <- value

    [<DefaultValue>]
    val mutable private code: string
    member this.Code
        with get () = this.code
        and set (value) = this.code <- value
