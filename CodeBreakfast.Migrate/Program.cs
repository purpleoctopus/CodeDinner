using CodeBreakfast.Common;
using CodeBreakfast.Data;
using CodeBreakfast.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((config) =>
    {
        config.AddJsonFile("appsettings.migrations.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(context.Configuration.GetConnectionString("SQLServer"));
        });
        services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    })
    .Build();

using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

var db = services.GetRequiredService<AppDbContext>();
Console.WriteLine("Starting database migrations...");
try
{
    await db.Database.MigrateAsync();
}
catch (Exception ex)
{
    Console.WriteLine("Error database migrations:");
    Console.WriteLine(ex.Message);
    return;
}
Console.WriteLine("Done database migrations.");

var userManager = services.GetRequiredService<UserManager<User>>();
var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
var configuration = services.GetRequiredService<IConfiguration>();

string roleName = AppRole.Admin.GetDescription();
string? adminUsername = configuration["Admin:Username"];
string? adminEmail = configuration["Admin:Email"];
string? adminPassword = configuration["Admin:Password"];

if (!await roleManager.RoleExistsAsync(roleName))
{
    await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
    Console.WriteLine("Role 'Admin' created");
}

Console.WriteLine("Attempting to create root user admin...");
var adminExists = await userManager.GetUsersInRoleAsync(roleName);
if (adminExists.Count == 0)
{
    if (string.IsNullOrEmpty(adminUsername) || string.IsNullOrEmpty(adminPassword))
    {
        Console.WriteLine("Admin data is empty");
        return;
    }
    
    var user = new User
    {
        UserName = adminUsername,
        Email = adminEmail,
        EmailConfirmed = true,
        RegisteredOn = DateTime.UtcNow
    };

    var result = await userManager.CreateAsync(user, adminPassword);
    if (result.Succeeded)
    {
        await userManager.AddToRoleAsync(user, roleName);
        Console.WriteLine("Admin user created.");
    }

    else
    {
        Console.WriteLine("Failed to create admin user:");
        foreach (var error in result.Errors)
            Console.WriteLine($" - {error.Description}");
    }
}
else
{
    Console.WriteLine("Admin user already exists.");
}