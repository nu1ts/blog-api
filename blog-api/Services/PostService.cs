using blog_api.Data;
using blog_api.Exceptions;
using blog_api.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_api.Services;

public class PostService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly GarDbContext _garDbContext;

    public PostService(ApplicationDbContext dbContext, GarDbContext garDbContext)
    {
        _dbContext = dbContext;
        _garDbContext = garDbContext;
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
        Guid? userId
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
        
        var userLikes = userId.HasValue 
            ? await GetUserLikes(userId.Value) 
            : new List<Guid>();

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
            HasLike = userLikes.Contains(post.Id),
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
    
    public async Task<Guid> CreatePost(CreatePostDto createPostDto, User user)
    {
        if (createPostDto.AddressId.HasValue)
        {
            var isValidAddress = await IsAddressValid(createPostDto.AddressId.Value);
            if (!isValidAddress)
            {
                throw new AddressException();
            }
        }
        
        var existingTags = await _dbContext.Tags
            .Where(tag => createPostDto.Tags.Contains(tag.Id))
            .ToListAsync();

        if (existingTags.Count != createPostDto.Tags.Count)
        {
            throw new TagException();
        }
        
        var post = new Post
        {
            Id = Guid.NewGuid(),
            CreateTime = DateTime.UtcNow,
            Title = createPostDto.Title,
            Description = createPostDto.Description,
            ReadingTime = createPostDto.ReadingTime,
            Image = createPostDto.Image,
            AuthorId = user.Id,
            Author = user.FullName,
            CommunityId = null,
            CommunityName = null,
            AddressId = createPostDto.AddressId,
            Likes = 0,
            CommentsCount = 0,
            Tags = createPostDto.Tags,
            Comments = new List<Guid>()
        };
        
        _dbContext.Posts.Add(post);
        user.Posts!.Add(post.Id);

        await _dbContext.SaveChangesAsync();

        return post.Id;
    }
    
    public async Task<PostFullDto> GetPost(Guid postId, Guid? userId)
    {
        var post = await _dbContext.Posts
            .FirstOrDefaultAsync(p => p.Id == postId);

        if (post == null)
            throw new PostException(postId);

        var hasLike = userId.HasValue && _dbContext.Users
            .Any(u => u.Id == userId && u.Likes!.Contains(post.Id));
        
        var comments = await _dbContext.Comments
            .Where(c => post.Comments.Contains(c.Id))
            .Select(comment => new CommentDto
            {
                Id = comment.Id,
                CreateTime = comment.CreateTime,
                Content = comment.Content,
                ModifiedDate = comment.ModifiedDate,
                DeleteDate = comment.DeleteDate,
                AuthorId = comment.AuthorId,
                Author = comment.Author,
                SubComments = comment.SubComments
            })
            .ToListAsync();

        var tags = _dbContext.Tags
            .Where(tag => post.Tags.Contains(tag.Id))
            .Select(tag => new TagDto
            {
                Id = tag.Id,
                CreateTime = tag.CreateTime,
                Name = tag.Name
            })
            .ToList();

        var postDto = new PostFullDto
        {
            Id = post.Id,
            CreateTime = post.CreateTime,
            Title = post.Title,
            Description = post.Description,
            ReadingTime = post.ReadingTime,
            Image = post.Image,
            AuthorId = post.AuthorId,
            Author = post.Author,
            CommunityId = post.CommunityId,
            CommunityName = post.CommunityName,
            AddressId = post.AddressId,
            Likes = post.Likes,
            HasLike = hasLike,
            CommentsCount = post.CommentsCount,
            Tags = tags,
            Comments = comments
        };

        return postDto;
    }
    
    public async Task AddLike(Guid postId, Guid userId)
    {
        var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        if (post == null)
            throw new PostException(postId);
        
        // TODO: IsPostAvailable(community)

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            throw new UserException();
        
        if (user.Likes!.Contains(postId))
            throw new InvalidOperationException("User has already liked this post.");
        
        user.Likes.Add(postId);
        post.Likes += 1;

        await _dbContext.SaveChangesAsync();
    }
    
    public async Task RemoveLike(Guid postId, Guid userId)
    {
        var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        if (post == null)
            throw new PostException(postId);
        
        // TODO: IsPostAvailable(community)

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            throw new UserException();
        
        if (!user.Likes!.Contains(postId))
            throw new InvalidOperationException("User has not liked this post.");
        
        user.Likes.Remove(postId);
        post.Likes -= 1;

        await _dbContext.SaveChangesAsync();
    }

    
    private async Task<bool> IsAddressValid(Guid addressId)
    {
        var addressExists = await _garDbContext.AsAddrObjs.AnyAsync(a => a.Objectguid == addressId);
        var houseExists = await _garDbContext.AsHouses.AnyAsync(a => a.Objectguid == addressId);
        return addressExists || houseExists;
    }
    
    private async Task<List<Guid>> GetUserLikes(Guid userId)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == userId)
            .Select(u => u.Likes)
            .FirstOrDefaultAsync();

        return user ?? new List<Guid>();
    }

    private static IQueryable<Post> Filter(
        IQueryable<Post> query,
        List<Guid>? tags,
        string? author,
        int? min,
        int? max,
        bool onlyMyCommunities,
        Guid? userId
    )
    {
        if (tags != null && tags.Any())
        {
            var tagList = tags.ToList();
            query = query.Where(p => p.Tags.Any(t => tagList.Contains(t)));
        }

        if (!string.IsNullOrEmpty(author))
        {
            query = query.Where(p => p.Author.ToUpper().Contains(author, StringComparison.OrdinalIgnoreCase));
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