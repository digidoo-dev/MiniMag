using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiniMag.Data;
using MiniMag.Models;
var builder = WebApplication.CreateBuilder(args);


// Ścieżka do SQLite zależna od środowiska
var env = builder.Environment;
string dbPath;

if (env.IsDevelopment())
{
    // lokalnie - folder App_Data w projekcie
    var appDataPath = Path.Combine(env.ContentRootPath, "App_Data");
    if (!Directory.Exists(appDataPath))
        Directory.CreateDirectory(appDataPath);

    dbPath = Path.Combine(appDataPath, "MiniMagContext-58f70efb-1f34-4349-9ad9-f0247088269b.db");
}
else
{
    // Azure Free / Linux - zapis w /home/data
    var homePath = Environment.GetEnvironmentVariable("HOME") ?? "/home";
    var azureDataPath = Path.Combine(homePath, "data");
    if (!Directory.Exists(azureDataPath))
        Directory.CreateDirectory(azureDataPath);

    dbPath = Path.Combine(azureDataPath, "MiniMagContext-58f70efb-1f34-4349-9ad9-f0247088269b.db");
}



builder.Services.AddDbContext<MiniMagContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}")
    .WithStaticAssets();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MiniMagContext>();
    db.Database.Migrate();
}

// Seed Database with data if it is empty. 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}


app.Run();
