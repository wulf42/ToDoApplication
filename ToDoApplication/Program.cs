using Microsoft.EntityFrameworkCore;
using ToDoApplication.Context;
using ToDoApplication.Services;
using ToDoApplication.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependency injection
builder.Services.AddScoped<ITaskToDoService, TaskToDoService>();

// Register dbcontext
builder.Services.AddDbContext<ToDoApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ToDoAppDatabase")));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TaskToDo}/{action=Index}/{id?}");

app.Run();