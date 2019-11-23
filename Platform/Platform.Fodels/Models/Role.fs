namespace Platform.Fodels.Models

open Platform.Fodels.Attributes
open Platform.Fodels.Enums
open Platform.Fodels.Interfaces

type [<AllowNullLiteral>] [<MenuAttribute("Role", PermissionNamesForFodels.RoleView, Sections.Administration, "role", Icons.Cart)>] Role private () =
    interface IEntityBase with
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
    
    override this.Equals(thatObj) =
            match thatObj with
            | :? Role as that -> 
                this.RoleName = that.RoleName
                && this.Description = that.Description
            | _ -> false
