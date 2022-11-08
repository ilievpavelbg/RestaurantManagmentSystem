using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Models.ApplicationUser;

namespace RestaurantManagmentSystem.Controllers
{
    public class ApplicationUserController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ApplicationUserCreateModel();

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(ApplicationUserCreateModel model)
        {
            return View();
        }
    }
}
