﻿namespace Platform.Fodels.Models.Address

open Platform.Fodels.Enums
open System
open Microsoft.EntityFrameworkCore.Infrastructure
open Platform.Fodels.Interfaces
open Platform.Fodels.Attributes

type [<AllowNullLiteral>] [<MenuAttribute("Квартиры", PermissionNamesForFodels.ViewModels, Sections.Models, "Apartment", Icons.Home)>] Apartment() =
    member this.Id
        with get () = this.id
        and set (value) = this.id <- value

    interface IEntityBase with
        member this.Id
            with get () = this.Id
            and set (value) = this.Id <- value

    member this.Name
        with get () = this.name
            and set (name) =
                if not (isNull name) then
                    this.name <- name
                else
                    raise (new ArgumentNullException(nameof (name)))

    interface IAddressElement with
        member this.Name
            with get () = this.Name
            and set (value) = this.Name <- value
        member this.Type = AddressItem.Apartment
        
    private new(lazyLoader: ILazyLoader) as newApartment =
        Apartment()
        then
            newApartment.LazyLoader <- lazyLoader
        
    [<DefaultValue>]
    val mutable private lazyLoader: ILazyLoader
    member this.LazyLoader
        with get () = this.lazyLoader
        and set (loader) =
            this.lazyLoader <- loader

    [<DefaultValue>]
    val mutable private id: int

    [<DefaultValue>]
    val mutable private name: string

    [<DefaultValue>]
    val mutable private house: House
    member this.House
        with get () = this.house
        and set (house) =
            if not (isNull house) then
                this.house <- house
            else
                raise (new ArgumentNullException(nameof (house)))
