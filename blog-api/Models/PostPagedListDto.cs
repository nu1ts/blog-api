namespace blog_api.Models;

public class PostPagedListDto
{
    public List<PostDto> Posts { get; set; }
    public PageInfoModel Pagination { get; set; }
}