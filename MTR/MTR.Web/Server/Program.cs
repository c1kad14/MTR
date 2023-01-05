using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

using MTR.Core.Abstractions;
using MTR.Core;
using MTR.DAL;
using MTR.Web.Shared;
using MediatR;
using MTR.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var path = System.IO.Directory.GetCurrentDirectory();
builder.Services.AddDbContext<MTRContext>(options => { options.UseSqlite($"Data Source={path}/mtr.db"); });
builder.Services.AddIdentity<MTRUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<MTRContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = false;
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder.WithOrigins("https://localhost:7010").AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add services to the container
var mapperConfig = new MapperConfiguration(c => c.AddProfile<MTRProfile>());
builder.Services.AddSingleton(mapperConfig.CreateMapper());
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddTransient<ICardManager, CardManager>();
builder.Services.AddTransient<IRoundManager, RoundManager>();
builder.Services.AddTransient<IActionManager, ActionManager>();
builder.Services.AddTransient<IPlayerManager, PlayerManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
