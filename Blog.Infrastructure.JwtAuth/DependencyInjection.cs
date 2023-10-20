namespace Microsoft.Extensions.DependencyInjection;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Blog.Infrastructure.Auth.Common.Abstractions;
using Blog.Infrastructure.JwtAuth.Common.Abstractions;
using Blog.Infrastructure.JwtAuth.Services;
using Blog.Infrastructure.JwtAuth.Common.Options;

public static class DependencyInjection
{
    public static IServiceCollection AddJwtAuth(
        this IServiceCollection services,
        JwtAuthOptions options
    )
    {
        services.AddSingleton(options);

        services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = getTokenValidationParameters(options);
            });

        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IAuthResponseProvider, JwtAuthResponseProvider>();

        return services;
    }

    private static TokenValidationParameters getTokenValidationParameters(JwtAuthOptions options)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = options.Issuer,
            ValidateAudience = true,
            ValidAudience = options.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = options.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    }
}
