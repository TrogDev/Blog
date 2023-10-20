namespace Blog.Application.Services.Comments;

using Microsoft.EntityFrameworkCore;

using FluentValidation;

using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Application.Common.Abstractions;
using Blog.Application.Common.Commands.Comments;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Extensions;
using Blog.Application.Common.Exceptions;
using Blog.Application.Services.Common;
using Blog.Application.Services.Comments.Factories;

public class CommentService : ICommentService
{
    private readonly IApplicationDbContext context;
    private readonly IValidator<CommentCreateCommand> createCommandValidator;

    public CommentService(
        IApplicationDbContext context,
        IValidator<CommentCreateCommand> createCommandValidator
    )
    {
        this.context = context;
        this.createCommandValidator = createCommandValidator;
    }

    public async Task<PaginatedList<CommentDTO>> GetList(CommentGetListCommand command)
    {
        var handler = new GetListHandler<CommentDTO, CommentGetListCommand>(
            new CommentGetListHandlerFactory(context)
        );
        return await handler.GetListQuery(command).GetPaginatedListAsync(command);
    }

    public async Task<long> Create(CommentCreateCommand command)
    {
        await createCommandValidator.ValidateAndThrowAsync(command);

        var comment = new Comment()
        {
            Content = command.Content,
            AuthorId = command.SenderId,
            PostId = command.PostId
        };

        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();

        return comment.Id;
    }

    public async Task Delete(CommentDeleteCommand command)
    {
        Comment comment = await getComment(command.Id, command.PostId);

        if (!isCanEdit(comment, command))
        {
            throw new ForbiddenException();
        }

        context.Comments.Remove(comment);
        await context.SaveChangesAsync();
    }

    private async Task<Comment> getComment(long id, long postId)
    {
        Comment? comment = await context.Comments.FirstOrDefaultAsync(
            e => e.Id == id & e.PostId == postId
        );

        if (comment is null)
        {
            throw new EntityNotFoundException();
        }

        return comment;
    }

    private bool isCanEdit(Comment comment, IAuthorizedCommand command)
    {
        return comment.AuthorId == command.SenderId || command.SenderRole == Role.Admin;
    }
}
