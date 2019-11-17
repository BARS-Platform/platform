namespace Platform.Fatabase

open Microsoft.EntityFrameworkCore
open Platform.Fodels
open Platform.Fodels.Interfaces
open Platform.Fodels.Models
open Platform.Fodels.Models.Address

type ApplicationDbContext() =
    inherit DbContext()

    override __.OnConfiguring optionsBuilder =
        optionsBuilder.UseNpgsql((new ApplicationConfiguration()).ConnectionString)
        ()

    override __.OnModelCreating modelBuilder =
        modelBuilder.Entity<User>().HasKey(fun (u: User) -> ((u :> IEntityBase).Id) :> obj) |> ignore
        modelBuilder.Entity<Apartment>().HasKey(fun (u: Apartment) -> ((u :> IEntityBase).Id) :> obj) |> ignore
        modelBuilder.Entity<House>().HasKey(fun (u: House) -> ((u :> IEntityBase).Id) :> obj) |> ignore
        modelBuilder.Entity<Street>().HasKey(fun (u: Street) -> ((u :> IEntityBase).Id) :> obj) |> ignore
        modelBuilder.Entity<City>().HasKey(fun (u: City) -> ((u :> IEntityBase).Id) :> obj) |> ignore
        modelBuilder.Entity<State>().HasKey(fun (u: State) -> ((u :> IEntityBase).Id) :> obj) |> ignore
        modelBuilder.Entity<Country>().HasKey(fun (u: Country) -> ((u :> IEntityBase).Id) :> obj) |> ignore

        base.OnModelCreating(modelBuilder)

    [<DefaultValue>]
    val mutable users: DbSet<User>
    member x.Users
        with get () = x.users
        and set v = x.users <- v

    [<DefaultValue>]
    val mutable apartments: DbSet<Apartment>
    member x.Apartments
        with get () = x.apartments
        and set v = x.apartments <- v

    [<DefaultValue>]
    val mutable countries: DbSet<Country>
    member x.Countries
        with get () = x.countries
        and set v = x.countries <- v

    [<DefaultValue>]
    val mutable cities: DbSet<City>
    member x.Cities
        with get () = x.cities
        and set v = x.cities <- v

    [<DefaultValue>]
    val mutable states: DbSet<State>
    member x.States
        with get () = x.states
        and set v = x.states <- v

    [<DefaultValue>]
    val mutable houses: DbSet<House>
    member x.Houses
        with get () = x.houses
        and set v = x.houses <- v

    [<DefaultValue>]
    val mutable streets: DbSet<Street>
    member x.Streets
        with get () = x.streets
        and set v = x.streets <- v