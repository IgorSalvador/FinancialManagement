using System.Globalization;
using FinancialManagement.Business.Core.Notifications;
using FinancialManagement.Infra.Data.Context;
using FinancialManagement.WebApp.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var cultureInfo = new CultureInfo(builder.Configuration["AppSettings:AppCulture"]!);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
