namespace Platform.Fodels.Models

open Platform.Fodels.Interfaces

type [<AllowNullLiteral>] RolePermission private () =
    interface IEntityBase with
        member this.Id
            with get () = this.id
            and set (value) = this.id <- value

    public new(role: Role, permission: Permission) as newUserRole =
        RolePermission()
        then
            newUserRole.Role <- role
            newUserRole.Permission <- permission
            
    [<DefaultValue>]
    val mutable private id: int
    member this.Id
            with get () = this.id
            and set (value) = this.id <- value
    
    [<DefaultValue>]
    val mutable private role: Role
    member this.Role
        with get () = this.role
        and set (value) = this.role <- value

    [<DefaultValue>]
    val mutable private permission: Permission
    member this.Permission
        with get () = this.permission
        and set value = this.permission <- value