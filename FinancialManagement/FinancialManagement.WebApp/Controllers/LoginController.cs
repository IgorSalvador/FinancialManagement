using System.Security.Claims;
using FinancialManagement.Business.Core.Notifications;
using FinancialManagement.Business.Models;
using FinancialManagement.Business.Models.IRepositories;
using FinancialManagement.Business.Models.Services.IServices;
using FinancialManagement.WebApp.Models.GeneralModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagement.WebApp.Controllers
{
    [Authorize]
    public class LoginController : BaseController
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService,
                               INotificator notificator) : base(notificator)
        {
            _userService = userService;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Index()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var claims = await _userService.Auth(model.Username, model.Password);

            if(!ValidOperation()) return View(model);

            if (claims == null)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro durante a autenticação, tente novamente mais tarde!");
                return View(model);
            }

            await Autheticate(claims);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Login");
        }

        private async Task Autheticate(List<Claim> claims)
        {
            var claimIdentity = new ClaimsPrincipal(
                new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

            var authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.Now.AddHours(4),
                IssuedUtc = DateTime.Now,
                IsPersistent = true,
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimIdentity, authProperties);
        }
    }
}
