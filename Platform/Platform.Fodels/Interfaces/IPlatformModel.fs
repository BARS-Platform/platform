namespace Platform.Fodels.Interfaces

type [<AllowNullLiteral>] IPlatformModel =
    interface
        abstract member Id : int with get,set
    end
