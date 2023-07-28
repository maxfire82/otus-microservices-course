using Microsoft.EntityFrameworkCore;
using Otus.Configuration;
using Otus.Users;

var builder = WebApplication.CreateBuilder(args);

// Configuration
// TODO will be refactored
var configuration = builder.Configuration;
var password = configuration.GetValueOrThrow<string>("USERS_DB_PASSWORD");
var host = configuration.GetValueOrThrow<string>("USERS_DB_HOST");
var database = configuration.GetValueOrThrow<string>("USERS_DB_NAME");
var username = configuration.GetValueOrThrow<string>("USERS_DB_USERNAME");
var port = configuration.GetValue<string>("USERS_DB_PORT");
var shouldCreateDb = configuration.GetValue<bool?>("USERS_DB_MIGRATE") ?? false;

var userDbConnection = $"Server={host};Database={database};Port={port};User Id={username};Password={password}"; 

// Add services to the container.
builder.Services.AddUserServices(userDbConnection);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (shouldCreateDb)
{
    using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
    {
        var serviceProvider = serviceScope?.ServiceProvider;
        var context = serviceProvider?.GetRequiredService<UserDbContext>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();