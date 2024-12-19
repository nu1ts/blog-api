using blog_api.Data;
using blog_api.Models;

namespace blog_api.Services;

public class AuthorService
{
    private readonly ApplicationDbContext _dbContext;

    public AuthorService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<AuthorDto> GetAuthors()
    {
        var authors = _dbContext.Users
            .Where(u => u.Posts!.Any())
            .OrderBy(u => u.FullName)
            .Select(u => new AuthorDto
            {
                FullName = u.FullName,
                BirthDate = u.BirthDate,
                Gender = u.Gender,
                Posts = u.Posts!.Count,
                Likes = u.Likes!.Count,
                Created = u.CreateTime
            })
            .ToList();

        return authors;
    }
}