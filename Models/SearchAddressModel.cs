namespace blog_api.Models;

public class SearchAddressModel
{
    public long ObjectId { get; set; }
    public Guid ObjectGuid { get; set; }
    public string? Text { get; set; }
    public GarAddressLevel? ObjectLevel { get; set; }
    public string? ObjectLevelText { get; set; }
}