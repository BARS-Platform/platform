namespace Platform.Fodels.Models

open Platform.Fodels.Attributes
open Platform.Fodels.Enums
open Platform.Fodels.Interfaces

// [<MenuAttribute("Permission", PermissionNamesForFodels.PermissionView, Sections.Administration, "Permission", Icons.Admin)>] 
type [<AllowNullLiteral>] Permission private () =
    interface IEntityBase with
        member this.Id
            with get () = this.id
            and set (value) = this.id <- value

    public new(permissionId: string, description: string) as newPermission =
        Permission()
        then
            newPermission.PermissionId <- permissionId            
            newPermission.Description <- description
            
    [<DefaultValue>]
    val mutable private id: int
    member this.Id
            with get () = this.id
            and set (value) = this.id <- value
    
    [<DefaultValue>]
    val mutable private permissionId: string
    member this.PermissionId
        with get () = this.permissionId
        and set (value) = this.permissionId <- value

    [<DefaultValue>]
    val mutable private description: string
    member this.Description
        with get () = this.description
        and set (value) = this.description <- value
        
    override this.Equals(thatObj) =
            match thatObj with
            | :? Permission as that -> 
                this.PermissionId = that.PermissionId
                && this.Description = that.Description    
            | _ -> false