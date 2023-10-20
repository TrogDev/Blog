namespace Blog.Web.Common.Extensions.Configuration;

using Blog.Infrastructure.Database;

public static class InitialiserExtensions
{
    public static IApplicationBuilder InitialiseDatabase(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        var initialiser =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        initialiser.Initialise();

        return app;
    }
}
