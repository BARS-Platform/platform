namespace Platform.Fatabase

open Platform.Models.Interfaces
open Platform.Fatabase
open System.Linq

type BaseRepository<'T when 'T :> IPlatformModel and 'T: not struct>(context: ApplicationDbContext) =
    interface IRepository<'T> with
    
        member this.Create(entity: 'T) =
            let query = async {
                context.Add(entity) |> ignore
                context.SaveChanges true |> ignore
                return entity            
            }
            Async.StartAsTask<'T>(query)

        member this.Delete(entity: 'T) =
            let query = async {
                context.Remove entity |> ignore
                return context.SaveChanges() > 0 
            }
            Async.StartAsTask query

        member this.Get id =
            let query = async {
                return context.Find(id)
            }
            Async.StartAsTask(query)

        member this.FindByPredicate expression  =
            let query = async {
                return context.Set<'T>().SingleOrDefault()
            }
            Async.StartAsTask(query)

        member this.Update (entity: 'T) =
            let query = async {
                context.Update(entity) |> ignore
                context.SaveChanges true |> ignore
                return entity
            }
            Async.StartAsTask(query)
