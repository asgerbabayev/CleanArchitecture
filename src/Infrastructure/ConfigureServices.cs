using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Services;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("local"),
            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitializer>();


        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        services.AddTransient<IDateTime, DateTimeService>();
        return services;
    }
}

//https://restcountries.com/