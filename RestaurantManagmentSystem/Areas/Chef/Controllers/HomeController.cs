using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagmentSystem.Areas.Chef.Controllers
{
    [Area("Chef")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
