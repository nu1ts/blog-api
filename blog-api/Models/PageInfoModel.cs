using System.ComponentModel.DataAnnotations;

namespace blog_api.Models;

public class PageInfoModel
{
    [Range(0, int.MaxValue, ErrorMessage = "Size must be positive")]
    public int Size { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Count must be positive")]
    public int Count { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Current must be positive")]
    public int Current { get; set; }
}