using System.Text.Json.Serialization;
using Application.Configurations;
using BookCrossingBackEnd.Configuration;
using BookCrossingBackEnd.Middleware;
using DataAccess.Context;
using DataAccess.Repositories.DiConfiguration;
using Microsoft.EntityFrameworkCore;
using NLog;
using WebApi.Controllers;
using WebApi.Mappers.Configurations;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

builder.Services.AddSystemServices();
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddApplicationMappers();
builder.Services.AddIdentityAndAuthorization();

builder.Services.AddDbContext<BookCrossingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddControllers(options =>
    {
        options.SuppressAsyncSuffixInActionNames = false;
    })
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    .AddApplicationPart(typeof(CategoryController).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(SystemServicesConfiguration.AllowedOrigins);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();