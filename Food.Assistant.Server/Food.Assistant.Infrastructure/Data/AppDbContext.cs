using Food.Assistant.Core.Recipes.Structure;
using Microsoft.EntityFrameworkCore;

namespace Food.Assistant.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Recipe> Recipes => Set<Recipe>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id)
                .HasConversion(id => id.Value, value => new RecipeId(value))
                .HasMaxLength(50);
            
            e.Property(x => x.Name).HasMaxLength(200);
        });
    }
}