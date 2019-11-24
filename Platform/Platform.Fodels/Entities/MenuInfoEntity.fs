namespace Platform.Fodels.Entities
open Platform.Fodels.Enums

type [<AllowNullLiteral>] MenuInfoEntity() =
    
    public new(description: string, permissionId: PermissionNamesForFodels, sectionName: Sections, link: string, icon: Icons) as newMenuInfo =
        MenuInfoEntity()
        then
            newMenuInfo.Description <- description
            newMenuInfo.PermissionId <- permissionId
            newMenuInfo.Section <- sectionName
            newMenuInfo.Link <- link
            newMenuInfo.Icon <- icon
    
    [<DefaultValue>]
    val mutable private description: string
    member this.Description
        with get () = this.description
        and set (value) = this.description <- value
        
    [<DefaultValue>]
    val mutable private permissionId: PermissionNamesForFodels
    member this.PermissionId
        with get () = this.permissionId
        and set (value) = this.permissionId <- value
        
    [<DefaultValue>]
    val mutable private section: Sections
    member this.Section
        with get () = this.section
        and set (value) = this.section <- value
    
    [<DefaultValue>]
    val mutable private link: string
    member this.Link
        with get () = this.link
        and set (value) = this.link <- value
        
    [<DefaultValue>]
    val mutable private icon: Icons
    member this.Icon
        with get () = this.icon
        and set (value) = this.icon <- value