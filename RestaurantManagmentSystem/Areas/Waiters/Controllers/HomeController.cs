using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Areas.Waiters.Models;

namespace RestaurantManagmentSystem.Areas.Waiters.Controllers
{
    [Area("Waiters")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Product[] productArray = new Product[] {
                new Product { Name = "Pants", Quantity = 5, Price=100 },
                new Product { Name = "Shirts", Quantity = 10, Price=80 },
                new Product { Name = "Shoes", Quantity = 15, Price=50 }
            };
            return View(productArray);
        }
    }
}
