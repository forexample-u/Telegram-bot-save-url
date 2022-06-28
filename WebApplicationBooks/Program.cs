using WebApplicationBooks.Domain;
using WebApplicationBooks.Domain.Repository;
using WebApplicationBooks.Domain.Repository.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();

builder.Services.AddControllersWithViews();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection));

var app = builder.Build();

app.MapControllerRoute("default", "{controller=Home}/{action=index}");

app.Run();