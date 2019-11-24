namespace Platform.Fodels.Entities

open System.Collections.Generic

type [<AllowNullLiteral>] MenuEntity() =
    
    public new(title: string, link: string, icon: string, children: IEnumerable<MenuEntity>) as newMenuInfo =
        MenuEntity()
        then
            newMenuInfo.Title <- title
            newMenuInfo.Link <- link
            newMenuInfo.Icon <- icon
            newMenuInfo.Children <- children
    
    [<DefaultValue>]
    val mutable private title: string
    member this.Title
        with get () = this.title
        and set (value) = this.title <- value
        
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
    val mutable private children: IEnumerable<MenuEntity>
    member this.Children
        with get () = this.children
        and set (value) = this.children <- value