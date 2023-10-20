namespace Blog.Web.Common.RequestData.Tags;

using Microsoft.AspNetCore.Mvc;

public class TagUpdateRequestData
{
    [FromRoute]
    public required long Id { get; set; }
    public required string Title { get; set; }
}