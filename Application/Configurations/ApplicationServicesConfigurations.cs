using Application.Services;
using Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configurations;

public static class ApplicationServicesConfigurations
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ICategoryService, CategoryService>();
    }
}