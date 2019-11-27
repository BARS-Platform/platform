namespace Platform.Fatabase

open Microsoft.EntityFrameworkCore
open Platform.Fodels
open Platform.Fodels.Interfaces
open System
open Platform.Fodels.Models
open Platform.Fodels.Models.Address

type ApplicationDbContext() =
    inherit DbContext()

    override __.OnConfiguring optionsBuilder =
        optionsBuilder.UseNpgsql((new ApplicationConfiguration()).ConnectionString)
        ()

    [<DefaultValue>]
    val mutable users: DbSet<User>
    member x.Users
        with get () = x.users
        and set v = x.users <- v

    [<DefaultValue>]
    val mutable roles: DbSet<Role>
    member x.Roles
        with get () = x.roles
        and set v = x.roles <- v

    [<DefaultValue>]
    val mutable userRoles: DbSet<UserRole>
    member x.UserRoles
        with get () = x.userRoles
        and set v = x.userRoles <- v

    [<DefaultValue>]
    val mutable permissions: DbSet<Permission>
    member x.Permissions
        with get () = x.permissions
        and set v = x.permissions <- v
        
    [<DefaultValue>]
    val mutable rolePermissions: DbSet<RolePermission>
    member x.RolePermissions
        with get () = x.rolePermissions
        and set v = x.rolePermissions <- v

    [<DefaultValue>]
    val mutable countries: DbSet<Country>
    member x.Countries
        with get () = x.countries
        and set v = x.countries <- v

    [<DefaultValue>]
    val mutable states: DbSet<State>
    member x.States
        with get () = x.states
        and set v = x.states <- v
        
    [<DefaultValue>]
    val mutable cities: DbSet<City>
    member x.Cities
        with get () = x.cities
        and set v = x.cities <- v

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
        
    [<DefaultValue>]
    val mutable apartments: DbSet<Apartment>
    member x.Apartments
        with get () = x.apartments
        and set v = x.apartments <- v

    [<DefaultValue>]
    val mutable addresses: DbSet<Address>
    member x.Addresses
        with get () = x.addresses
        and set v = x.addresses <- v