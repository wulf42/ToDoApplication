using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoApplication.Context;
using ToDoApplication.Models;
using ToDoApplication.Services;
using ToDoApplication.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



// Dependency injection
builder.Services.AddScoped<IShoppingProductService, ShoppingProductService>();
builder.Services.AddScoped<ITaskToDoService, TaskToDoService>();
builder.Services.AddScoped<ICalendarService, CalendarService>();
builder.Services.AddScoped<IWeatherApiService, WeatherApiService>();
builder.Services.AddScoped<IAccountService, AccountService>();


// Register dbcontext
builder.Services.AddDbContext<ToDoApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ToDoAppDatabase")));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});


// User Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<ToDoApplicationDbContext>();





var app = builder.Build();

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

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TaskToDo}/{action=Index}/{id?}");

app.Run();