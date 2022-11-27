using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Models.MenuItems;

namespace RestaurantManagmentSystem.Areas.Waiter.Controllers
{
    [Area("Waiter")]
    public class HomeController : Controller
    {
        private readonly ICategory categoryServise;
        private readonly IMenuItem menuItemService;
        /// <summary>
        /// Initialize category and manu services in constructor
        /// </summary>
        /// <param name="_category"></param>
        /// <param name="_menuItem"></param>
        public HomeController(ICategory _category, IMenuItem _menuItem)
        {
            categoryServise = _category;
            menuItemService = _menuItem;
        }

        /// <summary>
        /// Show all MenuItems
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var allMemuItem = new MultipleMenuItemViewModel();

            allMemuItem.ActiveMenuItems = await menuItemService.GetAllMenuItemsAsync();
            allMemuItem.DeletedMenuItems = await menuItemService.GetAllDeletedMenuItemsAsync();

            return View(allMemuItem);
        }
    }
}
