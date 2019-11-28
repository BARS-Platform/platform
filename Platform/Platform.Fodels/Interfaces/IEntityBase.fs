namespace Platform.Fodels.Interfaces

type [<AllowNullLiteral>] IEntityBase =
    interface
        abstract member Id : int with get,set
    end
