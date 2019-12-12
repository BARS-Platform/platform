namespace Platform.Fodels.Models

open Microsoft.EntityFrameworkCore.Infrastructure
open Platform.Fodels.Interfaces

type [<AllowNullLiteral>] UserRole private () =
    interface IEntityBase with
        member this.Id
            with get () = this.id
            and set (value) = this.id <- value

    public new(user: User, role: Role) as newUserRole =
        UserRole()
        then
            newUserRole.User <- user
            newUserRole.Role <- role
            
    private new(lazyLoader: ILazyLoader) as newUserRole =
        UserRole()
        then
            newUserRole.LazyLoader <- lazyLoader
        
    [<DefaultValue>]
    val mutable private lazyLoader: ILazyLoader
    member this.LazyLoader
        with get () = this.lazyLoader
        and set (loader) =
            this.lazyLoader <- loader
            
    [<DefaultValue>]
    val mutable private id: int
    member this.Id
            with get () = this.id
            and set (value) = this.id <- value
    
    [<DefaultValue>]
    val mutable private user: User
    member this.User
        with get () = this.user
        and set (value) = this.user <- value

    [<DefaultValue>]
    val mutable private role: Role
    member this.Role
        with get () = this.role
        and set value = this.role <- value