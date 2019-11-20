﻿namespace Platform.Fatabase

open Platform.Fodels.Interfaces
open Platform.Fatabase
open System.Linq

type BaseRepository(context: ApplicationDbContext) =
    interface IRepository with

        member this.Create<'T when 'T :> IEntityBase and 'T: not struct>(entity: 'T) =
            context.Add(entity)
            context.SaveChanges()
            entity

        member this.Delete<'T when 'T :> IEntityBase and 'T: not struct>(entity: 'T) =
            context.Remove entity
            context.SaveChanges() > 0

        member this.Get id =
            context.Find id

        member this.FindByPredicate<'T when 'T :> IEntityBase and 'T: not struct> expression =
            context.Set<'T>().SingleOrDefault expression

        member this.Update<'T when 'T :> IEntityBase and 'T: not struct>(entity: 'T) =
            context.Update entity
            context.SaveChanges()
            entity
