using CodeDinner.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodeDinner.API.Data;

public class AppDbContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Module> Modules { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Course>().HasIndex(el => el.Name).IsUnique();
    //}
}