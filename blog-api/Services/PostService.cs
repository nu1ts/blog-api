using blog_api.Data;
using blog_api.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_api.Services;

public class PostService
{
    private readonly ApplicationDbContext _dbContext;

    public PostService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PostPagedListDto> GetPostList(
        List<Guid>? tags,
        string? author,
        int? min,
        int? max,
        PostSorting? sorting,
        bool onlyMyCommunities,
        int page,
        int size,
        Guid userId
    )
    {
        ValidatePaginationAndFiltering(page, size, min, max);
        var query = _dbContext.Posts.AsQueryable();
        
        query = Filter(query, tags, author, min, max, onlyMyCommunities, userId);
        query = Sort(query, sorting);
        
        var totalCount = await query.CountAsync();
        
        var pagesCount = (int)Math.Ceiling((double)totalCount / size);
        ValidatePage(page, pagesCount == 0 ? 1 : pagesCount);
        
        var posts = await Pagination(query, page, size);

        var postDto = posts.Select(post => new PostDto
        {
            Id = post.Id,
            CreateTime = post.CreateTime,
            Title = post.Title,
            Description = post.Description,
            ReadingTime = post.ReadingTime,
            Image = post.Image,
            AuthorId = post.AuthorId,
            Author = post.Author,
            Likes = post.Likes,
            HasLike = post.Likes > 0,
            CommentsCount = post.CommentsCount,
            Tags = _dbContext.Tags
                .Where(tag => post.Tags.Contains(tag.Id))
                .Select(tag => new TagDto
                {
                    Name = tag.Name,
                    Id = tag.Id,
                    CreateTime = tag.CreateTime
                }).ToList()
        }).ToList();

        return new PostPagedListDto
        {
            Posts = postDto,
            Pagination = new PageInfoModel
            {
                Size = size,
                Count = totalCount,
                Current = page
            }
        };
    }

    private static IQueryable<Post> Filter(
        IQueryable<Post> query,
        List<Guid>? tags,
        string? author,
        int? min,
        int? max,
        bool onlyMyCommunities,
        Guid userId
    )
    {
        if (tags != null && tags.Any())
        {
            var tagList = tags.ToList();
            query = query.Where(p => p.Tags.Any(t => tagList.Contains(t)));
        }

        if (!string.IsNullOrEmpty(author))
        {
            query = query.Where(p => p.Author.ToUpper().Contains(author.ToUpper()));
        }

        if (min.HasValue)
        {
            query = query.Where(p => p.ReadingTime >= min.Value);
        }

        if (max.HasValue)
        {
            query = query.Where(p => p.ReadingTime <= max.Value);
        }
        
        if (onlyMyCommunities)
        {
            // TODO: remake with communities
        }

        return query;
    }

    private static IQueryable<Post> Sort(IQueryable<Post> query, PostSorting? sorting)
    {
        return sorting switch
        {
            PostSorting.LikeAsc => query.OrderBy(p => p.Likes),
            PostSorting.LikeDesc => query.OrderByDescending(p => p.Likes),
            PostSorting.CreateAsc => query.OrderBy(p => p.CreateTime),
            PostSorting.CreateDesс => query.OrderByDescending(p => p.CreateTime),
            _ => query
        };
    }
    
    private static async Task<List<Post>> Pagination(IQueryable<Post> query, int page, int size)
    {
        return await query.Skip((page - 1) * size)
                           .Take(size)
                           .ToListAsync();
    }
    
    private static void ValidatePaginationAndFiltering(int page, int size, int? min, int? max)
    {
        if (page <= 0)
        {
            throw new ArgumentException("Page must be greater than zero");
        }

        if (size <= 0)
        {
            throw new ArgumentException("Size must be greater than zero");
        }
        
        if (min.HasValue && min.Value < 0)
        {
            throw new ArgumentException("Min reading time must be greater than or equal to zero");
        }

        if (max.HasValue && max.Value < 0)
        {
            throw new ArgumentException("Max reading time must be greater than or equal to zero");
        }
        
        if (min.HasValue && max.HasValue && min.Value > max.Value)
        {
            throw new ArgumentException("Min reading time cannot be greater than Max reading time");
        }
    }
    
    private static void ValidatePage(int page, int pagesCount)
    {
        if (page > pagesCount)
        {
            throw new ArgumentException($"Requested page {page} exceeds the total number of pages {pagesCount}");
        }
    }
}