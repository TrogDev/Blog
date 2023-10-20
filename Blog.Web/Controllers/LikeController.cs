namespace Blog.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.Commands.Likes;
using Blog.Application.Common.Exceptions;
using Blog.Web.Common.Extensions;

[ApiController]
public class LikeController : ControllerBase
{
    private readonly ILikeService likeService;

    public LikeController(ILikeService likeService)
    {
        this.likeService = likeService;
    }

    [HttpPost]
    [Route("posts/{postId:long}/likes")]
    [Authorize]
    public async Task<IActionResult> Create([FromRoute] long postId)
    {
        var command = new LikeCreateCommand()
        {
            PostId = postId,
            SenderId = User.GetUserId(),
            SenderRole = User.GetUserRole()
        };
        await likeService.Create(command);
        return StatusCode(201);
    }

    [HttpDelete]
    [Route("posts/{postId:long}/likes")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] long postId)
    {
        var command = new LikeDeleteCommand()
        {
            PostId = postId,
            SenderId = User.GetUserId(),
            SenderRole = User.GetUserRole()
        };

        try
        {
            await likeService.Delete(command);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (ForbiddenException)
        {
            return Forbid();
        }

        return NoContent();
    }

    [HttpGet]
    [Authorize]
    [Route("likes/check")]
    public async Task<IActionResult> CheckLikes([FromQuery] ICollection<long> postIds)
    {
        var command = new LikeCheckCommand()
        {
            PostIds = postIds,
            SenderId = User.GetUserId(),
            SenderRole = User.GetUserRole()
        };
        return Ok(await likeService.CheckLikes(command));
    }
}
