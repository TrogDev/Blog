namespace Blog.Web.Common.RequestData.Comments;

using Microsoft.AspNetCore.Mvc;

public class CommentGetListByUserRequestData
{
    [FromRoute]
    public required long AuthorId { get; set; }
    public int Page { get; set; } = 1;
    public int PerPage { get; set; } = 10;
}