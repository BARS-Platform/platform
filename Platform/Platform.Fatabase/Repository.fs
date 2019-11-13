namespace Platform.Fatabase

open Platform.Fodels.Interfaces
open Platform.Fatabase
open System.Linq

type BaseRepository<'T when 'T :> IPlatformModel and 'T: not struct>(context: ApplicationDbContext) =
    interface IRepository<'T> with
    
        member this.Create(entity: 'T) =
            context.Add(entity)
            context.SaveChanges()
            entity

        member this.Delete(entity: 'T) =
            context.Remove entity
            context.SaveChanges() > 0

        member this.Get id =
            context.Find id

        member this.FindByPredicate expression  =
            context.Set<'T>().SingleOrDefault expression

        member this.FindAllByPredicate expression  =
            context.Set<'T>().Where expression

        member this.Update (entity: 'T) =
            context.Update entity
            context.SaveChanges
            entity
