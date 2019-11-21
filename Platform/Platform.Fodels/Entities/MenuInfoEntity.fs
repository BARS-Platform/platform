namespace Platform.Fodels.Entities

type [<AllowNullLiteral>] MenuInfoEntity() =
    
    public new(link: string, icon: string, description: string) as newMenuInfo =
        MenuInfoEntity()
        then
            newMenuInfo.Link <- link
            newMenuInfo.Icon <- icon
            newMenuInfo.Description <- description
    
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
    
    [<DefaultValue>]
    val mutable private description: string
    member this.Description
        with get () = this.description
        and set (value) = this.description <- value