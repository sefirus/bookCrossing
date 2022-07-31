using Core.Entities;
using DataAccess.Context;
using IdentityServer.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.ServiceExtensions;

public static class ServicesInstaller
{
    public static void AddServices(this IServiceCollection services, IConfiguration config)
    {

        services.AddDbContext<BookCrossingContext>(opts => 
            opts.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddIdentity<User, IdentityRole<int>>(opts =>
            {
                opts.Password.RequireDigit = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<BookCrossingContext>()
            .AddDefaultTokenProviders();

        var assembly = typeof(Program).Assembly.GetName().Name;

        services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddOperationalStore(opts =>
            {
                opts.ConfigureDbContext = builder => builder.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                    opt => opt.MigrationsAssembly(assembly));
            })
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryApiScopes(Config.GetApiScopes())
            .AddInMemoryClients(Config.GetClients())
            .AddAspNetIdentity<User>();

        services.AddScoped<IProfileService, ProfileService>();
    }
}