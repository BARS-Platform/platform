namespace Platform.Fatabase

open System
open Platform.Fodels.Interfaces
open Platform.Fatabase
open System.Linq
open System.Runtime.InteropServices
open Microsoft.Win32.SafeHandles

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
            
        member this.GetAll() =
            context.Set<'T>().AsQueryable()
            
        member this.GetWithRelated<'T when 'T :> IEntityBase and 'T: not struct> (id, includeFunc)  =
            includeFunc.Invoke(context.Set<'T>()).SingleOrDefault(fun t -> t.Id = id)

        member this.FindByPredicate<'T when 'T :> IEntityBase and 'T: not struct> expression =
            context.Set<'T>().SingleOrDefault expression
         
        member this.FindByPredicate<'T when 'T :> IEntityBase and 'T: not struct> (expression, includeFunc)  =
            includeFunc.Invoke(context.Set<'T>()).SingleOrDefault expression

        member this.FindAllByPredicate<'T when 'T :> IEntityBase and 'T: not struct> expression  =
            context.Set<'T>().Where expression
            
        member this.FindAllByPredicate<'T when 'T :> IEntityBase and 'T: not struct> (expression, includeFunc)  =
            includeFunc.Invoke(context.Set<'T>()).Where expression

        member this.Update<'T when 'T :> IEntityBase and 'T: not struct>(entity: 'T) =
            context.Update entity
            context.SaveChanges()
            entity
            
    [<DefaultValue>]
    val mutable private _disposed: bool
    member private __._handle : SafeHandle = new SafeFileHandle(IntPtr.Zero, true) :> SafeHandle
    
    interface IDisposable with
        member __.Dispose() =
            __.Dispose(true);
            GC.SuppressFinalize __
            
    member __.Dispose(disposing: bool) =
            if __._disposed then ()
            else
                if disposing then __._handle.Dispose()
                else __._disposed <- true