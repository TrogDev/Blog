namespace Blog.Web.Common.RequestData.Posts;

using Microsoft.AspNetCore.Mvc;

public class PostUpdateRequestData
{
    [FromRoute]
    public required long Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public string? PreviewImage { get; set; }
    public required long CategoryId { get; set; }
}