using HarSA.AspNetCore.Mvc.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace HarSA.AspNetCore.Mvc.Controllers
{
    [AutoValidateAntiforgeryToken]
    public abstract class BaseController : Controller
    {
        protected INotificationService Notification { get; private set; }

        public BaseController(INotificationService notificationService)
        {
            Notification = notificationService;
        }
    }
}
