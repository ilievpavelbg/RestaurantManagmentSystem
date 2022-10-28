using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Models;

namespace RestaurantManagmentSystem.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly ICategory category;
        private readonly IMenuItem menuItem;

        public MenuItemController(ICategory _category, IMenuItem _menuItem)
        {
            category = _category;
            menuItem = _menuItem;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            var menu = new AddMenuItemViewModel()
            {
                Categories = category.GetAllCategories()
            };

            return View(menu);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMenuItemViewModel model)
        {
            await menuItem.AddMenuItemAsync(model);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult All()
        {
            var allMemuItem = menuItem.GetAllMenuItems();

            return View(allMemuItem);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await menuItem.EditMenuItemViewAsync(id);
            model.Categories = category.GetAllCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditMenuItemViewModel model)
        {
           await menuItem.EditMenuItemAsync(model);

            return RedirectToAction("All");
        }

        
        public async Task<IActionResult> Delete(int Id)
        {
            await menuItem.DeleteMenuItemAsync(Id);

            return RedirectToAction("All");
        }

    }
}
