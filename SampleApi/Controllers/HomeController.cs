using HarSA.AspNetCore.Mvc.Controllers;
using HarSA.AspNetCore.Mvc.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(INotificationService notificationService) : base(notificationService)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}