namespace Platform.Fodels.Models

open System;
open Platform.Fodels.Interfaces
open Platform.Configuration.Enums
open Platform.Fodels.Attributes

type [<AllowNullLiteral>] WeatherForecast() =
    interface IPlatformModel with
        member this.Id
            with get () = this.id
            and set (value) = this.id <- value

    [<DefaultValue>]
    val mutable private id: int
    member this.Id
        with get () = this.id
        and set (value) = this.id <- value

    [<DefaultValue>]
    [<Platform(AttributesEnum.Form ||| AttributesEnum.Grid)>]
    val mutable private date: DateTime
    member this.Date
        with get () = this.date
        and set (value) = this.date <- value

    [<DefaultValue>]
    [<Platform(AttributesEnum.Grid)>]
    val mutable private tempC: int
    member this.TemperatureC
        with get () = this.tempC
        and set (value) = this.tempC <- value

    [<DefaultValue>]
    val mutable private tempF: int
    member this.TemperatureF
        with get () = this.tempF
        and set (value) = this.tempF <- value

    [<DefaultValue>]
    [<Platform(AttributesEnum.Form)>]
    val mutable private myProp: int
    member this.MyProperty
        with get () = this.myProp
        and set (value) = this.myProp <- value

    [<DefaultValue>]
    val mutable private sum: string
    member public this.Summary
        with get () = this.sum
        and set value = this.sum <- value

