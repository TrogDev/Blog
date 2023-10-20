namespace Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using FluentValidation;

using Blog.Application.Common.Abstractions;
using Blog.Infrastructure.Database;
using Blog.Infrastructure.Auth.Common.Abstractions;
using Blog.Infrastructure.Auth.Services;
using Blog.Infrastructure.Email;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        addDatabaseServices(services, configuration);
        addEmailServices(services, configuration);
        addAuthServices(services);

        return services;
    }

    private static void addDatabaseServices(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        string connection = configuration.GetConnectionString("Default");

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection));

        services.AddScoped<IApplicationDbContext>(
            provider => provider.GetRequiredService<ApplicationDbContext>()
        );
        services.AddScoped<IAuthDbContext>(
            provider => provider.GetRequiredService<ApplicationDbContext>()
        );
        services.AddScoped<ApplicationDbContextInitialiser>();
    }

    private static void addEmailServices(IServiceCollection services, IConfiguration configuration)
    {
        EmailConfiguration emailConfig = configuration
            .GetRequiredSection("EmailConfiguration")
            .Get<EmailConfiguration>();
        services.AddSingleton(emailConfig);
        services.AddTransient<IEmailNotifier, EmailNotifier>();
    }

    private static void addAuthServices(IServiceCollection services)
    {
        services.AddTransient<IPasswordHasher, Sha256PasswordHasher>();
        services.AddTransient<IUserManager, UserManager>();
        services.AddTransient<IUserVerificator, UserVerificator>();
        services.AddTransient<IAuthService, AuthService>();
    }
}
