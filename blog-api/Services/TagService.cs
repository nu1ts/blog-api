using blog_api.Data;
using blog_api.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_api.Services;

public class TagService
{
    private readonly ApplicationDbContext _dbContext;

    public TagService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<TagDto>> GetTags()
    {
        return await _dbContext.Tags
            .AsNoTracking()
            .Select(tag => new TagDto
            {
                Name = tag.Name,
                Id = tag.Id,
                CreateTime = tag.CreateTime,
            })
            .ToListAsync();
    }
}