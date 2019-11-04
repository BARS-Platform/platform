namespace Platform.Fatabase

open Platform.Fodels.Interfaces
open Platform.Fatabase
open Microsoft.EntityFrameworkCore

type BaseRepository<'T when 'T :> IPlatformModel and 'T: not struct>(context: ApplicationDbContext) =
    interface IRepository<'T> with
    
        member this.Create(entity: 'T) =
            let q = async {
                context.Add entity |> ignore
                context.SaveChangesAsync |> ignore
                return entity
            }
            Async.StartAsTask<'T>(q)

        member this.Delete(entity: 'T) =
            let query = async {
                context.Remove entity |> ignore
                return context.SaveChanges() > 0
            }
            Async.StartAsTask query

        member this.Get id =
            context.FindAsync id

        member this.FindByPredicate expression  =
            context.Set<'T>().SingleOrDefaultAsync expression

        member this.Update (entity: 'T) =
            let query = async {
                context.Update entity |> ignore
                context.SaveChanges |> ignore
                return entity
            }
            Async.StartAsTask(query)
