using FinancialManagement.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FinancialManagement.Business.Core.Notifications;

namespace FinancialManagement.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(INotificator notificator,
                              ILogger<HomeController> logger) : base(notificator)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
