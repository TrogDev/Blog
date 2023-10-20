namespace Blog.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Blog.Domain.Enums;
using Blog.Application.Common.Abstractions;
using Blog.Application.Common.Commands.Categories;
using Blog.Application.Common.Exceptions;
using Blog.Web.Common.RequestData.Categories;
using Blog.Web.Common.Filters;

[ApiController]
[Route("/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] CategoryGetListCommand command)
    {
        return Ok(await categoryService.GetList(command));
    }

    [HttpGet]
    [Route("{id:long}")]
    public async Task<IActionResult> GetDetail([FromRoute] CategoryGetDetailCommand command)
    {
        try
        {
            return Ok(await categoryService.GetDetail(command));
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [Authorize]
    [Role(Role.Admin)]
    public async Task<IActionResult> Create([FromForm] CategoryCreateCommand command)
    {
        return StatusCode(201, await categoryService.Create(command));
    }

    [HttpDelete]
    [Route("{id:long}")]
    [Authorize]
    [Role(Role.Admin)]
    public async Task<IActionResult> Delete([FromRoute] CategoryDeleteCommand command)
    {
        try
        {
            await categoryService.Delete(command);
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
    public async Task<IActionResult> Update([FromForm] CategoryUpdateRequestData data)
    {
        var command = new CategoryUpdateCommand() { Id = data.Id, Title = data.Title };

        try
        {
            await categoryService.Update(command);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
