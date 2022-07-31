using IdentityServer.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
builder.Services.AddServices(config);
builder.Services.AddCors(config =>
    {
        config.AddDefaultPolicy(config => 
            config.WithOrigins("http://localhost:4200", "https://localhost:5001", "http://localhost:5283")
                .AllowAnyHeader().AllowAnyMethod());
    }
);

var app = builder.Build();
app.UseCors();
app.UseIdentityServer();
app.Run();