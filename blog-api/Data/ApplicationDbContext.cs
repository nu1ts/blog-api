using blog_api.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_api.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<BlacklistedToken> BlacklistedTokens { get; set; }
    
    public DbSet<User> Users { get; set; }
}