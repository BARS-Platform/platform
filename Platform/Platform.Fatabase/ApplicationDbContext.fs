namespace Platform.Fatabase

open Microsoft.EntityFrameworkCore

type ApplicationDbContext(options : DbContextOptions<ApplicationDbContext>) =
    inherit DbContext(options)
    
    [<DefaultValue>]
    val mutable users : DbSet<Platform.Models.User>
    member x.Users
        with get() = x.users
        and set v = x.users <- v
