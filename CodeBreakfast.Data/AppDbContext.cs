using System.Text.Json;
using CodeBreakfast.Data.Entities;
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
    public DbSet<Review> Reviews { get; set; }
    public DbSet<UserConfig> UserConfigs { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<Newsletter> Newsletters { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
        
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
        modelBuilder.Entity<UserLesson>()
            .HasOne(x => x.Lesson)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<UserCourse>()
            .HasOne(x => x.Course)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.Property(e => e.AdditionalData)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, jsonSerializerOptions),
                    v => JsonSerializer.Deserialize<object>(v, jsonSerializerOptions))
                .HasColumnType("TEXT");
        });
        modelBuilder.Entity<Comment>()
            .HasOne(x => x.Author)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Review>()
            .HasOne(x => x.Author)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Lesson>()
            .HasOne(x => x.Author)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Lesson>()
            .HasOne(x => x.Course)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}