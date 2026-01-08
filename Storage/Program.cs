using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Storage.Extensions;
using Storage.Models;
using Storage.Persistence.Data;
using Storage.Services;

// Set system culture to sv-SE
CultureInfo systemCulture = new("sv-SE");
CultureInfo.DefaultThreadCurrentCulture = systemCulture;
CultureInfo.DefaultThreadCurrentUICulture = systemCulture;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StorageContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StorageContext") ?? throw new InvalidOperationException("Connection string 'StorageContext' not found.")));

// Add services to the container.
//builder.Services.AddSingleton<MockDb>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else
{
    await app.SeedDataAsync();
}

    // Apply migrations when starting with a fresh DB
    // ToDo: Apply migrations in workflow before `deploy` job
    // using(var scope = app.Services.CreateScope())
    // {
    //     var db = scope.ServiceProvider.GetRequiredService<StorageContext>();
    //     db.Database.Migrate(); 
    // }

    app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}")
    .WithStaticAssets();

//DbInitializer.Seed(app);

app.Run();
