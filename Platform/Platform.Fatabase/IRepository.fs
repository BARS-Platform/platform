namespace Platform.Fatabase

open Microsoft.EntityFrameworkCore.Query
open System
open System.Linq.Expressions
open Platform.Fodels.Interfaces
open System.Linq

type IRepository =
    interface
        abstract Create<'T when 'T :> IEntityBase and 'T: not struct> : 'T -> 'T
        abstract Delete<'T when 'T :> IEntityBase and 'T: not struct>  : 'T -> bool
        abstract Get<'T when 'T :> IEntityBase and 'T: not struct> : int -> 'T
        abstract GetWithRelated<'T when 'T :> IEntityBase and 'T: not struct> : int * Func<IQueryable<'T>, IQueryable<'T>> -> 'T
        abstract FindByPredicate<'T when 'T :> IEntityBase and 'T: not struct> : Expression<Func<'T, bool>> -> 'T
        abstract Update<'T when 'T :> IEntityBase and 'T: not struct> : 'T -> 'T
        abstract FindByPredicate<'T when 'T :> IEntityBase and 'T: not struct> : Expression<Func<'T, bool>> * Func<IQueryable<'T>, IIncludableQueryable<'T, Object>> -> 'T
        abstract FindAllByPredicate<'T when 'T :> IEntityBase and 'T: not struct> : Expression<Func<'T, bool>> -> IQueryable<'T>
        abstract FindAllByPredicate<'T when 'T :> IEntityBase and 'T: not struct> : Expression<Func<'T, bool>> * Func<IQueryable<'T>, IIncludableQueryable<'T, Object>> -> IQueryable<'T>
    end
