namespace Platform.Fatabase

open System
open System.Linq.Expressions
open Platform.Fodels.Interfaces
open System.Linq

type IRepository<'T when 'T :> IPlatformModel> =
    interface
        abstract Create: 'T -> 'T
        abstract Delete: 'T -> bool
        abstract Get: (int) -> 'T 
        abstract FindByPredicate: Expression<Func<'T, bool>> -> 'T
        abstract FindAllByPredicate: Expression<Func<'T, bool>> -> IQueryable<'T>
        abstract Update: 'T -> 'T
    end
