using blog_api.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_api.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<BlacklistedToken> BlacklistedTokens { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Tag> Tags { get; set; }
    
    public DbSet<Post> Posts { get; set; }
    
    public DbSet<Comment> Comments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Tag>().HasData(
            new Tag { Id = Guid.NewGuid(), CreateTime = DateTime.UtcNow, Name = "история" },
            new Tag { Id = Guid.NewGuid(), CreateTime = DateTime.UtcNow, Name = "еда" },
            new Tag { Id = Guid.NewGuid(), CreateTime = DateTime.UtcNow, Name = "18+" },
            new Tag { Id = Guid.NewGuid(), CreateTime = DateTime.UtcNow, Name = "приколы" },
            new Tag { Id = Guid.NewGuid(), CreateTime = DateTime.UtcNow, Name = "it" },
            new Tag { Id = Guid.NewGuid(), CreateTime = DateTime.UtcNow, Name = "интернет" },
            new Tag { Id = Guid.NewGuid(), CreateTime = DateTime.UtcNow, Name = "теория_заговора" },
            new Tag { Id = Guid.NewGuid(), CreateTime = DateTime.UtcNow, Name = "соцсети" },
            new Tag { Id = Guid.NewGuid(), CreateTime = DateTime.UtcNow, Name = "косплей" },
            new Tag { Id = Guid.NewGuid(), CreateTime = DateTime.UtcNow, Name = "преступление" }
        );
    }
}