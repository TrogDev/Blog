namespace Blog.Web;

using Blog.Infrastructure.JwtAuth.Common.Options;
using Blog.Web.Common.Extensions.Configuration;

internal class Startup 
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration config)
    {
        Configuration = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();

        services.AddInfrastructureServices(Configuration);
        services.AddJwtAuth(new JwtAuthOptions()
        {
            Issuer = "https://trogdev.ru/",
            Audience = "trogdev.ru",
            PrivateKey = "SecretSecretSecretSecretSecretSecretSecret",
            AccessLifeTimeSeconds = 60 * 60 * 24 * 30
        });
        services.AddApplicationServices();
        services.AddExceptionHandlers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAuthentication();

        app.UseRouting();

        app.UseAuthorization();

        app.UseExceptionHandlers();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.InitialiseDatabase();
    }
}