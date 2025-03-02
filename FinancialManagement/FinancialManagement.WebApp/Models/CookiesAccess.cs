using FinancialManagement.Infra.Data.Context;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.WebApp.Models;

public class CookiesAccess
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public bool IsLoggedIn { get; set; }
    public bool IsLoginExpired { get; set; }

    public CookiesAccess(AppDbContext context,
                         IHttpContextAccessor httpContextAccessor,
                         IConfiguration configuration)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        Initialize();
    }

    public void Initialize()
    {
        try
        {
            var user = _httpContextAccessor.HttpContext!.User;

            if (user.Identity!.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)user.Identity;
                var claims = identity.Claims;

                UserId = int.Parse(claims?.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value!);
                Name = claims?.Where(x => x.Type == ClaimTypes.Name).First().Value;
                Email = claims?.Where(x => x.Type == ClaimTypes.Email).First().Value;
                Role = claims?.Where(x => x.Type == ClaimTypes.Role).First().Value;
                IsLoggedIn = true;
                IsLoginExpired = false;

                _context.Database.GetDbConnection().Close();
            }
            else
                throw new Exception();
        }
        catch
        {
            UserId = 0;
            Name = string.Empty;
            Email = string.Empty;
            Role = string.Empty;
            IsLoggedIn = false;
            IsLoginExpired = true;
        }
    }
}

public static class CurrentCookies
{
    public static int UserId { get; set; }
    public static string Name { get; set; }
    public static string Email { get; set; }
    public static string Role { get; set; }
    public static bool IsLoggedIn { get; set; }

    public static void GetCookies(CookiesAccess cookies)
    {
        cookies.Initialize();

        UserId = cookies.UserId;
        Name = cookies.Name;
        Email = cookies.Email;
        Role = cookies.Role;
        IsLoggedIn = cookies.IsLoggedIn;
    }
}