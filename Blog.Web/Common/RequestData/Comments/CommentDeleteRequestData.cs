namespace Blog.Web.Common.RequestData.Comments;

using Microsoft.AspNetCore.Mvc;

public class CommentDeleteRequestData
{
    [FromRoute]
    public required long PostId { get; set; }
    [FromRoute]
    public required long Id { get; set; }
}