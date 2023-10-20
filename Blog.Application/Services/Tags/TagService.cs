namespace Blog.Application.Services.Tags;

using FluentValidation;

using Blog.Domain.Entities;
using Blog.Application.Common.Abstractions;
using Blog.Application.Common.Commands.Tags;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Extensions;
using Blog.Application.Common.Exceptions;
using Blog.Application.Services.Common;
using Blog.Application.Services.Tags.Factories;

public class TagService : ITagService
{
    private readonly IApplicationDbContext context;
    private readonly IValidator<TagCreateCommand> createCommandValidator;
    private readonly IValidator<TagUpdateCommand> updateCommandValidator;

    public TagService(
        IApplicationDbContext context,
        IValidator<TagCreateCommand> createCommandValidator,
        IValidator<TagUpdateCommand> updateCommandValidator
    )
    {
        this.context = context;
        this.createCommandValidator = createCommandValidator;
        this.updateCommandValidator = updateCommandValidator;
    }

    public async Task<PaginatedList<TagDTO>> GetList(TagGetListCommand command)
    {
        var handler = new GetListHandler<TagDTO, TagGetListCommand>(
            new TagGetListHandlerFactory(context)
        );
        return await handler.GetListQuery(command).GetPaginatedListAsync(command);
    }

    public async Task<TagDTO> GetDetail(TagGetDetailCommand command)
    {
        var handler = new GetDetailHandler<TagDTO, TagGetDetailCommand>(
            new TagGetDetailHandlerFactory(context)
        );
        return await handler.GetDetail(command);
    }

    public async Task<long> Create(TagCreateCommand command)
    {
        await createCommandValidator.ValidateAndThrowAsync(command);

        var tag = new Tag() { Title = command.Title, };

        await context.Tags.AddAsync(tag);
        await context.SaveChangesAsync();

        return tag.Id;
    }

    public async Task Delete(TagDeleteCommand command)
    {
        Tag tag = await getTag(command.Id);
        context.Tags.Remove(tag);
        await context.SaveChangesAsync();
    }

    public async Task Update(TagUpdateCommand command)
    {
        await updateCommandValidator.ValidateAndThrowAsync(command);

        Tag Tag = await getTag(command.Id);

        Tag.Title = command.Title;

        await context.SaveChangesAsync();
    }

    private async Task<Tag> getTag(long TagId)
    {
        Tag? tag = await context.Tags.FindAsync(TagId);

        if (tag is null)
        {
            throw new EntityNotFoundException();
        }

        return tag;
    }
}
