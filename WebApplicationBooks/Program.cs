using WebApplicationBooks.Domain;
using WebApplicationBooks.Domain.Repository;
using WebApplicationBooks.Domain.Repository.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApplicationBooks.Models;

var builder = WebApplication.CreateBuilder();
builder.Host.UseSerilog();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<AccountUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .WriteTo.File("Logs/info.log",
        rollOnFileSizeLimit: true,
        fileSizeLimitBytes: (1024 * 1024) * 20, //20 ла
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 10000)
    .CreateLogger();


builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllersWithViews();


var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Home}/{action=index}");

app.Run();