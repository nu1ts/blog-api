using blog_api.Data;

namespace blog_api.Services;

public class DevelopmentDatabaseService
{
    private readonly ApplicationDbContext _dbContext;

    public DevelopmentDatabaseService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ClearUsersAsync()
    {
        _dbContext.Users.RemoveRange(_dbContext.Users);
        _dbContext.BlacklistedTokens.RemoveRange(_dbContext.BlacklistedTokens);
        _dbContext.Posts.RemoveRange(_dbContext.Posts);
        
        await _dbContext.SaveChangesAsync();
    }
}