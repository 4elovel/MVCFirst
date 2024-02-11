namespace MVCFirst.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    /*    public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }*/
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Data.sqlite;");
    }

}

