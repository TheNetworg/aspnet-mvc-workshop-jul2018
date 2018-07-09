using Microsoft.EntityFrameworkCore;
using Todo.DAL.Entities;

namespace Todo.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoItem> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=TodoDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}