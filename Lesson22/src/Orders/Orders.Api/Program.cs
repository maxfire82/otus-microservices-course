using System.Text.Json.Serialization;
using Common.Authentication;
using Common.Configuration;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using Prometheus.SystemMetrics;
using Orders.Api.Services;
using Orders.Domain;

var builder = WebApplication.CreateBuilder(args);

// Configuration
// TODO will be refactored
var configuration = builder.Configuration;
var password = configuration.GetValueOrThrow<string>("ORDERS_DB_PASSWORD");
var host = configuration.GetValueOrThrow<string>("ORDERS_DB_HOST");
var database = configuration.GetValueOrThrow<string>("ORDERS_DB_NAME");
var username = configuration.GetValueOrThrow<string>("ORDERS_DB_USERNAME");
var port = configuration.GetValue<string>("ORDERS_DB_PORT");
 var shouldCreateDb = configuration.GetValue<bool?>("ORDERS_DB_MIGRATE") ?? false;

var ordersDbConnection = $"Server={host};Database={database};Port={port};User Id={username};Password={password};Maximum Pool Size=100"; 

// Add services to the container.
builder.Services.AddDomainServices(ordersDbConnection);
builder.Services.AddCommonServices();
builder.Services.AuthService(configuration);
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
        var context = serviceProvider?.GetRequiredService<OrderDbContext>();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapMetrics();

app.Run();