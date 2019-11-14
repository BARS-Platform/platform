namespace Platform.Fatabase

open Microsoft.EntityFrameworkCore
open Platform.Fodels
open Platform.Fodels.Interfaces
open System
open Platform.Fodels.Models

type ApplicationDbContext() =
    inherit DbContext()

    [<DefaultValue>]
    val mutable users: DbSet<User>
    member x.Users
        with get () = x.users
        and set v = x.users <- v

    override __.OnConfiguring optionsBuilder =
        optionsBuilder.UseNpgsql((new ApplicationConfiguration()).ConnectionString)
        ()

    override __.OnModelCreating modelBuilder =
        modelBuilder.Entity<User>().HasKey(fun (u: User) -> ((u :> IPlatformModel).Id) :> obj)

        base.OnModelCreating(modelBuilder)
