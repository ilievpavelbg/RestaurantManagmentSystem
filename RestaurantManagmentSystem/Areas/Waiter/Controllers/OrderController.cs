using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Models.MenuItems;
using RestaurantManagmentSystem.Core.Services;

namespace RestaurantManagmentSystem.Areas.Waiter.Controllers
{
    [Area("Waiter")]
    public class OrderController : Controller
    {
        private readonly IMenuItem menuItemService;
        public OrderController(IMenuItem _menuItemService)
        {
            menuItemService = _menuItemService;
        }

        public async Task<IActionResult> Create()
        {
            var menu = await menuItemService.GetAllMenuItemsAsync();

            return View(menu);
        }
    }
}
