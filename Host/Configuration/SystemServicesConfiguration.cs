using Application.Services;
using Core.Entities;
using Core.Interfaces.Services;
using DataAccess.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BookCrossingBackEnd.Configuration;

public static class SystemServicesConfiguration
{
    public const string AllowedOrigins = "frontend";

    public static void AddSystemServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<ILoggerManager, LoggerManager>();

        services.AddCors(options =>
        {
            options.AddPolicy(name: AllowedOrigins,
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200");
                    policy.WithHeaders("*");
                    policy.WithMethods("*");
                    policy.AllowCredentials();
                    policy.WithExposedHeaders("X-Pagination");
                });
        });
    }

    public static void AddIdentityAndAuthorization(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<BookCrossingContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;

            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
        });

        services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:7030";
                options.Audience = "bookCrossingApi";
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero
                };
            });
    }
}