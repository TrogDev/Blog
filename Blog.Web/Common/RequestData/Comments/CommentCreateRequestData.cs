namespace Blog.Web.Common.RequestData.Comments;

using Microsoft.AspNetCore.Mvc;

public class CommentCreateRequestData
{
    [FromRoute]
    public required long PostId { get; set; }
    public required string Content { get; set; }
}