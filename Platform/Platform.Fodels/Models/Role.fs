namespace Platform.Fodels.Models

open System
open Platform.Fodels.Interfaces

type [<AllowNullLiteral>] Role() =
    interface IPlatformModel with
        member this.Id
            with get () = this.id
            and set (value) = this.id <- value

    public new(roleName: string, description: string) as newRole =
        Role()
        then
            newRole.RoleName <- roleName
            newRole.Description <- description

    [<DefaultValue>]
    val mutable private id: int
    member this.Id
            with get () = this.id
            and set (value) = this.id <- value

    [<DefaultValue>]
    val mutable private roleName: string
    member this.RoleName
        with get () = this.roleName
        and set (value) = this.roleName <- value

    [<DefaultValue>]
    val mutable private description: string
    member this.Description
        with get () = this.description
        and set (value) = this.description <- value

    [<DefaultValue>]
    val mutable private permissions: List<Permission>
    member this.Permissions
        with get () = this.permissions
        and set (value) = this.permissions <- value
