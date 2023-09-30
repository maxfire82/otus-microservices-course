using Auth.Api.Services;
using Auth.Domain;
using Common.Configuration;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using Prometheus.SystemMetrics;
using Users.Domain;

var builder = WebApplication.CreateBuilder(args);

// Configuration
// TODO will be refactored
var configuration = builder.Configuration;
var password = configuration.GetValueOrThrow<string>("AUTH_DB_PASSWORD");
var host = configuration.GetValueOrThrow<string>("AUTH_DB_HOST");
var database = configuration.GetValueOrThrow<string>("AUTH_DB_NAME");
var username = configuration.GetValueOrThrow<string>("AUTH_DB_USERNAME");
var port = configuration.GetValue<string>("AUTH_DB_PORT");
var shouldCreateDb = configuration.GetValue<bool?>("AUTH_DB_MIGRATE") ?? false;

var authDbConnection = $"Server={host};Database={database};Port={port};User Id={username};Password={password};Maximum Pool Size=100"; 

// Add services to the container.
builder.Services.AddAuthServices(authDbConnection);
builder.Services.AddCommonServices();
builder.Services.AddControllers();
builder.Services.AddSystemMetrics();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (shouldCreateDb)
{
    using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
    {
        var serviceProvider = serviceScope?.ServiceProvider;
        var context = serviceProvider?.GetRequiredService<AuthDbContext>();
        if (context == null)
        {
            return;
        }
        
        context.Database.SetCommandTimeout(TimeSpan.FromSeconds(10));
        await context.Database.MigrateAsync();
        
        return;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpMetrics();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapMetrics();

app.Run();