namespace Blog.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Blog.Domain.Enums;
using Blog.Application.Common.Abstractions;
using Blog.Application.Common.Commands.Comments;
using Blog.Application.Common.Exceptions;
using Blog.Web.Common.RequestData.Comments;
using Blog.Web.Common.Filters;
using Blog.Web.Common.Extensions;

[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService commentService;

    public CommentController(ICommentService commentService)
    {
        this.commentService = commentService;
    }

    [HttpGet]
    [Route("posts/{postId:long}/comments")]
    public async Task<IActionResult> GetListByPost([FromQuery] CommentGetListByPostRequestData data)
    {
        var command = new CommentGetListCommand()
        {
            PostId = data.PostId,
            Page = data.Page,
            PerPage = data.PerPage
        };
        return Ok(await commentService.GetList(command));
    }

    [HttpGet]
    [Route("users/{authorId:long}/comments")]
    public async Task<IActionResult> GetListByUser([FromQuery] CommentGetListByUserRequestData data)
    {
        var command = new CommentGetListCommand()
        {
            AuthorId = data.AuthorId,
            Page = data.Page,
            PerPage = data.PerPage
        };
        return Ok(await commentService.GetList(command));
    }

    [HttpPost]
    [Route("posts/{postId:long}/comments")]
    [Authorize]
    [Role(Role.Verified, Role.Admin)]
    public async Task<IActionResult> Create([FromForm] CommentCreateRequestData data)
    {
        var command = new CommentCreateCommand()
        {
            Content = data.Content,
            PostId = data.PostId,
            SenderId = User.GetUserId(),
            SenderRole = User.GetUserRole()
        };
        return StatusCode(201, await commentService.Create(command));
    }

    [HttpDelete]
    [Route("posts/{postId:long}/comments/{id:long}")]
    [Authorize]
    [Role(Role.Verified, Role.Admin)]
    public async Task<IActionResult> Create([FromForm] CommentDeleteRequestData data)
    {
        var command = new CommentDeleteCommand()
        {
            Id = data.Id,
            PostId = data.PostId,
            SenderId = User.GetUserId(),
            SenderRole = User.GetUserRole()
        };

        try
        {
            await commentService.Delete(command);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
