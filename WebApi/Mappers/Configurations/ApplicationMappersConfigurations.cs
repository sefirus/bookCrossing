using Core.Entities;
using Core.Interfaces.Mappers;
using Core.ViewModels.CategoryViewModels;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Mappers.CategoryMappers;

namespace WebApi.Mappers.Configurations;

public static class ApplicationMappersConfigurations
{
    public static void AddApplicationMappers(this IServiceCollection services)
    {
        services.AddTransient<IVmMapper<Category, ReadCategoryViewModel>, ReadCategoryVmMapper>();
    }
}