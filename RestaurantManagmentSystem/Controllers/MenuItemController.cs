using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagmentSystem.Controllers
{
    public class MenuItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Add()
        //{
        //    return View();
        //}
    }
}
