namespace Platform.Fodels.Models

open System;
open Platform.Fodels.Interfaces
open Platform.Fodels.Enums
open Platform.Fodels.Attributes

type [<AllowNullLiteral>] WeatherForecast() =
    interface IPlatformModel with
        member this.Id
            with get () = this.id
            and set (value) = this.id <- value

    [<DefaultValue>]
    val mutable private id: int

    [<DefaultValue>]
    val mutable private date: DateTime
    
    [<Platform(AttributesEnum.Form ||| AttributesEnum.Grid)>]
    member this.Date
        with get () = this.date
        and set (value) = this.date <- value

    [<DefaultValue>]
    val mutable private tempC: int
    
    [<Platform(AttributesEnum.Grid)>]
    member this.TemperatureC
        with get () = this.tempC
        and set (value) = this.tempC <- value

    [<DefaultValue>]
    val mutable private tempF: int
    member this.TemperatureF
        with get () = this.tempF
        and set (value) = this.tempF <- value

    [<DefaultValue>]
    val mutable private myProp: int
    
    [<Platform(AttributesEnum.Form)>]
    member this.MyProperty
        with get () = this.myProp
        and set (value) = this.myProp <- value

    [<DefaultValue>]
    val mutable private sum: string
    member public this.Summary
        with get () = this.sum
        and set value = this.sum <- value

