using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoriesOfTheLand.Controllers;
using StoriesOfTheLand.Data;
using StoriesOfTheLand.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<StoriesOfTheLandContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("StoriesOfTheLandContext") ?? throw new InvalidOperationException("Connection string 'StoriesOfTheLandContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<SponsorService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;


    SeedData.Initialize(services);

}




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
