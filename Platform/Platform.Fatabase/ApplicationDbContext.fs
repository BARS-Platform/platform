namespace Platform.Fatabase

open Microsoft.EntityFrameworkCore
open Platform.Fodels
open Platform.Fodels.Models

type ApplicationDbContext() =
    inherit DbContext()

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

    override __.OnConfiguring optionsBuilder =
        optionsBuilder.UseNpgsql((new ApplicationConfiguration()).ConnectionString)
        ()

    override __.OnModelCreating modelBuilder =
        modelBuilder.Entity<User>().ToTable("Users")
        modelBuilder.Entity<Role>().ToTable("Roles")
        modelBuilder.Entity<UserRole>().ToTable("UserRoles")
        modelBuilder.Entity<Permission>().ToTable("Permissions")
        modelBuilder.Entity<RolePermission>().ToTable("RolePermissions")

        base.OnModelCreating(modelBuilder)
