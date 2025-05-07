using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository;

public class RepositoryContext : IdentityDbContext<User>
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ExerciseConfiguration());
        modelBuilder.ApplyConfiguration(new BodyPartConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new AvatarConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }

    public DbSet<Exercise>? Exercises { get; set; }
    public DbSet<BodyPart>? BodyParts { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Avatar>? Avatars { get; set; }
}