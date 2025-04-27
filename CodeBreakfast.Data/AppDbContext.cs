using CodeBreakfast.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeBreakfast.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.Content).HasColumnType("TEXT");
        });
        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.Property(e => e.HtmlContent).HasColumnType("TEXT");
        });
        modelBuilder.Entity<UserLesson>(entity =>
        {
            entity.Property(e => e.AdditionalJson).HasColumnType("TEXT");
        });
    }
}