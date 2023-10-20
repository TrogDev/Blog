namespace Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using FluentValidation;

using Blog.Application.Common.Abstractions;
using Blog.Application.Services.Posts;
using Blog.Application.Services.Categories;
using Blog.Application.Services.Tags;
using Blog.Application.Services.Comments;
using Blog.Application.Services.Likes;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient<IPostService, PostService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<ITagService, TagService>();
        services.AddTransient<ICommentService, CommentService>();
        services.AddTransient<ILikeService, LikeService>();

        return services;
    }
}
