# Web API Demo with Entity Framework


## Steps
1. Create DbContext
2. Configure to use local db
3. Run migration and update database schema
4. Register DbContext for dependency injection
5. Implement your own service that uses the database
6. Replace it in DI resolver (service locator)
7. [You're done](https://d1u5p3l4wpay3k.cloudfront.net/battlerite_gamepedia_en/c/cf/VO_Vanguard_Ultimate_8.mp3)

## Hints

### Nuggets
Select DAL as default project in the package manager console
```
Install-Package Microsoft.EntityFrameworkCore -Version 2.1.1
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 2.1.1
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 2.1.1
```

### Code hints
```cs
public class AppDbContext : DbContext
{
    public DbSet<TodoItem> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\mssqllocaldb;Database=TodoDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            // In production, never use connection string inside code, use some appsettings - this is only for the sake of the demo
    }
}
```

Create code first database

### Package manager console commands
Select DAL as default project in the package manager console
```
Add-Migration -Name "Initial schema"
Update-Database
```