using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Models.MenuItems;

namespace RestaurantManagmentSystem.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly ICategory category;
        private readonly IMenuItem menuItem;
        /// <summary>
        /// Initialize category and manu services in constructor
        /// </summary>
        /// <param name="_category"></param>
        /// <param name="_menuItem"></param>
        public MenuItemController(ICategory _category, IMenuItem _menuItem)
        {
            category = _category;
            menuItem = _menuItem;
        }
        /// <summary>
        /// MenuItem Index page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Add MenuItem View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            var menu = new AddMenuItemViewModel()
            {
                Categories = category.GetAllCategories()
            };

            return View(menu);
        }
        /// <summary>
        /// Add MenuItem to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddMenuItemViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await menuItem.AddMenuItemAsync(model);

            return RedirectToAction("All");
        }
        /// <summary>
        /// Show all MenuItems
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult All()
        {
            var allMemuItem = menuItem.GetAllMenuItems();

            return View(allMemuItem);
        }
        /// <summary>
        /// Edit MenuItem View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await menuItem.EditGetMenuItemAsync(id);

            model.Categories = category.GetAllCategories(); 

            return View(model);
        }
        /// <summary>
        /// Edit MenuItem, update database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditMenuItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await menuItem.EditPostMenuItemAsync(model);

            return RedirectToAction("All");
        }
        /// <summary>
        /// Delete MenuItem
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int Id)
        {
            await menuItem.DeleteMenuItemAsync(Id);

            return RedirectToAction("All");
        }

    }
}
