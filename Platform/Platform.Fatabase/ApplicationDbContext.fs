namespace Platform.Fatabase

open Microsoft.EntityFrameworkCore
open Platform.Fodels
open System
open System.Collections.Generic
open Platform.Fodels.Models

type ApplicationDbContext() =
    inherit DbContext()

    member this.MapEntityToNormalNames<'T when 'T: not struct>(modelBuilder: ModelBuilder) =
        Array.ForEach (typedefof<'T>.GetProperties(), fun property ->
            (
                modelBuilder.Entity<'T>().Property(property.Name).HasColumnName(property.Name.ToLower())
                ()
            ))

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

    override __.OnConfiguring optionsBuilder =
        optionsBuilder.UseNpgsql((new ApplicationConfiguration()).ConnectionString)
        ()

    override __.OnModelCreating modelBuilder =
        modelBuilder.Entity<User>().ToTable("users")
        modelBuilder.Entity<Role>().ToTable("roles")
        modelBuilder.Entity<UserRole>().ToTable("user_roles")
        modelBuilder.Entity<Permission>().ToTable("permissions")

        __.MapEntityToNormalNames<User>(modelBuilder);
        __.MapEntityToNormalNames<Role>(modelBuilder);
        __.MapEntityToNormalNames<UserRole>(modelBuilder);
        __.MapEntityToNormalNames<Permission>(modelBuilder);

        modelBuilder.Entity<User>().HasKey(fun (u: User) -> (u.Id) :> obj)
            .HasName("pk_id")
        modelBuilder.Entity<Permission>().HasKey(fun (permission: Permission) -> (permission.Id) :> obj)
            .HasName("pk_permission_id")
        modelBuilder.Entity<Role>().HasKey(fun (role: Role) -> (role.Id) :> obj)
            .HasName("pk_role_id")
        modelBuilder.Entity<Role>().HasMany(fun (role: Role) -> (role.Permissions) :> IEnumerable<'obj>)
            .WithOne()
        modelBuilder.Entity<UserRole>().HasKey(fun (userRole: UserRole) -> (userRole.Id) :> obj)
            .HasName("pk_user_role_id")

        base.OnModelCreating(modelBuilder)
