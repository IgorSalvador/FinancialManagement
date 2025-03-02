using FinancialManagement.Business.Core.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagement.WebApp.Controllers
{
    public class BaseController : Controller
    {
        private readonly INotificator _notificator;

        public BaseController(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected bool ValidOperation()
        {
            if (!_notificator.HasNotification()) return true;

            var notifications = _notificator.GetNotifications();
            notifications.ForEach(x => ViewData.ModelState.AddModelError(string.Empty, x.Message));
            return false;
        }
    }
}
