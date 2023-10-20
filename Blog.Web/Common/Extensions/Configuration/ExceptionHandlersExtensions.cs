namespace Blog.Web.Common.Extensions.Configuration;

using Blog.Web.Middlewares;

public static class ExceptionHandlersExtensions
{
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddSingleton<ValidationExceptionHandlerMiddleware>();
        return services;
    }

    public static IApplicationBuilder UseExceptionHandlers(this IApplicationBuilder app)  
    {  
        app.UseMiddleware<ValidationExceptionHandlerMiddleware>();
        return app;
    }
}
