using CleanArchitecture.WebUI.Filters;
using FluentValidation.AspNetCore;

namespace CleanArchitecture.WebUI;

public static class ConfigureServices
{
    
    public static IServiceCollection AddUIServices(this IServiceCollection services)
    {
        services.AddControllersWithViews(options => options.Filters.Add<ApiExceptionFilterAttribute>())
            .AddFluentValidation(opt => opt.AutomaticValidationEnabled = false);

        return services;
    }
}
