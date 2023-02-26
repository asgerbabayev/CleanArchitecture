using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CleanArchitecture.Application.IntegrationTests;
internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.ConfigureAppConfiguration(webBuilder =>
        {
            string path = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString(), @"appsettings.json");
            var integrationConfig = new ConfigurationBuilder()
            .AddJsonFile(path)
            .AddEnvironmentVariables()
            .Build();

            webBuilder.AddConfiguration(integrationConfig);
        });


        builder.ConfigureServices((builder, services) =>
        {
            services
                .Remove<DbContextOptions<ApplicationDbContext>>()
                .AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("test"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        });
    }
}
