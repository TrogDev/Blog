namespace Blog.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Blog.Domain.Enums;
using Blog.Application.Common.Abstractions;
using Blog.Application.Common.Commands.Posts;
using Blog.Application.Common.Exceptions;
using Blog.Web.Common.RequestData.Posts;
using Blog.Web.Common.Filters;

[ApiController]
[Route("/posts")]
public class PostController : ControllerBase
{
    private readonly IPostService postService;

    public PostController(IPostService postService)
    {
        this.postService = postService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PostGetListCommand command)
    {
        return Ok(await postService.GetList(command));
    }

    [HttpGet]
    [Route("{id:long}")]
    public async Task<IActionResult> GetDetail([FromRoute] PostGetDetailCommand command)
    {
        try
        {
            return Ok(await postService.GetDetail(command));
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [Authorize]
    [Role(Role.Admin)]
    public async Task<IActionResult> Create([FromForm] PostCreateCommand command)
    {
        return StatusCode(201, await postService.Create(command));
    }

    [HttpDelete]
    [Route("{id:long}")]
    [Authorize]
    [Role(Role.Admin)]
    public async Task<IActionResult> Delete([FromRoute] PostDeleteCommand command)
    {
        try
        {
            await postService.Delete(command);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut]
    [Route("{id:long}")]
    [Authorize]
    [Role(Role.Admin)]
    public async Task<IActionResult> Update([FromForm] PostUpdateRequestData data)
    {
        var command = new PostUpdateCommand()
        {
            Id = data.Id,
            Title = data.Title,
            Content = data.Content,
            PreviewImage = data.PreviewImage,
            CategoryId = data.CategoryId
        };

        try
        {
            await postService.Update(command);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut]
    [Route("{id:long}/tags")]
    [Authorize]
    [Role(Role.Admin)]
    public async Task<IActionResult> SetTags([FromForm] PostSetTagsRequestData data)
    {
        var command = new PostSetTagsCommand()
        {
            Id = data.Id,
            TagIds = data.TagIds
        };

        await postService.SetTags(command);

        return NoContent();
    }
}
