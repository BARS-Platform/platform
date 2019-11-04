namespace Platform.Fatabase

open Microsoft.EntityFrameworkCore
open Platform.Fodels
open System
open Platform.Fodels.Models

type ApplicationDbContext() =
    inherit DbContext()

    member this.MapEntityToNormalNames<'T when 'T: not struct>(modelBuilder: ModelBuilder) =
        Array.ForEach (typedefof<'T>.GetProperties(), fun property ->
            (
                modelBuilder.Entity<'T>().Property(property.Name).HasColumnName(property.Name.ToLower()) |> ignore
            ))

    [<DefaultValue>]
    val mutable users: DbSet<User>
    member x.Users
        with get () = x.users
        and set v = x.users <- v

    override __.OnConfiguring optionsBuilder =
        optionsBuilder.UseNpgsql((new ApplicationConfiguration()).ConnectionString) |> ignore

    override __.OnModelCreating modelBuilder =
        modelBuilder.Entity<User>().ToTable("users") |> ignore

        __.MapEntityToNormalNames<User>(modelBuilder);
        modelBuilder.Entity<User>().HasKey(fun (u: User) -> (u.Id) :> obj).HasName("pk_id") |> ignore

        base.OnModelCreating(modelBuilder)
