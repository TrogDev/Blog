namespace Blog.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Blog.Domain.Enums;
using Blog.Application.Common.Abstractions;
using Blog.Application.Common.Commands.Tags;
using Blog.Application.Common.Exceptions;
using Blog.Web.Common.RequestData.Tags;
using Blog.Web.Common.Filters;

[ApiController]
[Route("/tags")]
public class TagController : ControllerBase
{
    private readonly ITagService tagService;

    public TagController(ITagService tagService)
    {
        this.tagService = tagService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] TagGetListCommand command)
    {
        return Ok(await tagService.GetList(command));
    }

    [HttpGet]
    [Route("{id:long}")]
    public async Task<IActionResult> GetDetail([FromRoute] TagGetDetailCommand command)
    {
        try
        {
            return Ok(await tagService.GetDetail(command));
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [Authorize]
    [Role(Role.Admin)]
    public async Task<IActionResult> Create([FromForm] TagCreateCommand command)
    {
        return StatusCode(201, await tagService.Create(command));
    }

    [HttpDelete]
    [Route("{id:long}")]
    [Authorize]
    [Role(Role.Admin)]
    public async Task<IActionResult> Delete([FromRoute] TagDeleteCommand command)
    {
        try
        {
            await tagService.Delete(command);
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
    public async Task<IActionResult> Update([FromForm] TagUpdateRequestData data)
    {
        var command = new TagUpdateCommand() { Id = data.Id, Title = data.Title };

        try
        {
            await tagService.Update(command);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
