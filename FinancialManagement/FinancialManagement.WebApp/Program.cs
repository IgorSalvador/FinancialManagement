using System.Globalization;
using FinancialManagement.Business.Core.Notifications;
using FinancialManagement.Infra.Data.Context;
using FinancialManagement.WebApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var cultureInfo = new CultureInfo(builder.Configuration["AppSettings:AppCulture"]!);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Login/Index"); // 401 - Unauthorized
        options.AccessDeniedPath = new PathString("/Home/Error"); // 403 - Forbidden
        options.LogoutPath = new PathString("/Login/Logout");
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Expire in 30 minutes
        options.SlidingExpiration = true; // Renew time in all requests
    });


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// Registrar IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<CookiesAccess>();

builder.Services.AddScoped<INotificator, Notificator>();

var app = builder.Build();

var localizationOptions = new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture(app.Configuration["AppSettings:AppCulture"]!),
    SupportedCultures = new List<CultureInfo> { cultureInfo },
    SupportedUICultures = new List<CultureInfo> { cultureInfo }
};
app.UseRequestLocalization(localizationOptions);

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


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
