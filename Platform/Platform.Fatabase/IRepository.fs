namespace Platform.Fatabase

open System
open System.Linq.Expressions
open Platform.Fodels.Interfaces

type IRepository =
    interface
        abstract Create<'T when 'T :> IEntityBase and 'T: not struct> : 'T -> 'T
        abstract Delete<'T when 'T :> IEntityBase and 'T: not struct>  : 'T -> bool
        abstract Get<'T when 'T :> IEntityBase and 'T: not struct> : int -> 'T
        abstract FindByPredicate<'T when 'T :> IEntityBase and 'T: not struct> : Expression<Func<'T, bool>> -> 'T
        abstract Update<'T when 'T :> IEntityBase and 'T: not struct> : 'T -> 'T
    end
