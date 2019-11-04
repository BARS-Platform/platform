namespace Platform.Fatabase

open System
open System.Linq.Expressions
open System.Threading.Tasks
open Platform.Fodels.Interfaces

type IRepository<'T when 'T :> IPlatformModel> =
    interface
        abstract Create: 'T -> 'T Task
         abstract Delete: 'T -> Task<bool>
        abstract Get: (int) -> 'T ValueTask
        abstract FindByPredicate: Expression<Func<'T, bool>> -> 'T Task
        abstract Update: 'T -> Task<'T>
    end
