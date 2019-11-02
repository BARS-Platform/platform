namespace Platform.Fatabase

open System
open System.Linq.Expressions
open System.Threading.Tasks
open Platform.Fodels

type IRepository<'T when 'T :> IPlatformModel> =
    interface
        abstract Create: 'T -> 'T Task
         abstract Delete: 'T -> Task<bool>
        abstract Get: obj [] -> 'T Task
        abstract FindByPredicate: Expression<Func<'T, bool>> -> 'T Task
        abstract Update: 'T -> Task<'T>
    end
