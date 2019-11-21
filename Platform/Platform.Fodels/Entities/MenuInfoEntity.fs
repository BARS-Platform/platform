namespace Platform.Fodels.Entities

type [<AllowNullLiteral>] MenuInfoEntity() =
    
    public new(description: string, permissionId: string, sectionName: string, link: string, icon: string) as newMenuInfo =
        MenuInfoEntity()
        then
            newMenuInfo.Description <- description
            newMenuInfo.PermissionId <- permissionId
            newMenuInfo.SectionName <- sectionName
            newMenuInfo.Link <- link
            newMenuInfo.Icon <- icon
    
    [<DefaultValue>]
    val mutable private description: string
    member this.Description
        with get () = this.description
        and set (value) = this.description <- value
        
    [<DefaultValue>]
    val mutable private permissionId: string
    member this.PermissionId
        with get () = this.permissionId
        and set (value) = this.permissionId <- value
        
    [<DefaultValue>]
    val mutable private sectionName: string
    member this.SectionName
        with get () = this.sectionName
        and set (value) = this.sectionName <- value
    
    [<DefaultValue>]
    val mutable private link: string
    member this.Link
        with get () = this.link
        and set (value) = this.link <- value
        
    [<DefaultValue>]
    val mutable private icon: string
    member this.Icon
        with get () = this.icon
        and set (value) = this.icon <- value