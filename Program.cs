using coinbase.Hubs;
using coinbase.Models;
using Microsoft.EntityFrameworkCore;

// program.cs
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add SignalR
builder.Services.AddSignalR();

// Add DbContext using MySQL
builder.Services.AddDbContext<TapGameContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<EnergyHub>("/energyHub");

// Add the Boost route
app.MapControllerRoute(
    name: "boost",
    pattern: "Home/Boost",
    defaults: new { controller = "Home", action = "Boost" });

app.Run();