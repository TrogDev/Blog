namespace Blog.Web.Common.RequestData.Posts;

using Microsoft.AspNetCore.Mvc;

public class PostSetTagsRequestData
{
    [FromRoute]
    public required long Id { get; set; }
    public IEnumerable<long> TagIds { get; set; } = new List<long>();
}