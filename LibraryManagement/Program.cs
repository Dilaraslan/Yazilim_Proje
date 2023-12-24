using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibraryManagementContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LibraryManagementContext") ?? throw new InvalidOperationException("Connection string 'LibraryManagementContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    //var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        // Hata durumunda konsola mesaj yazdır
        Console.Error.WriteLine($"An error occurred during seeding the database: {ex.Message}");

        // veya
        // logger.LogError(ex, "An error occurred during seeding the database.");

        // İstisna fırlatma
        throw;
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
