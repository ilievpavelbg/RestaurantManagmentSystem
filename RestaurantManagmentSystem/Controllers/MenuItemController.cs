using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Models.MenuItems;

namespace RestaurantManagmentSystem.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class MenuItemController : Controller
    {
        private readonly ICategory categoryServise;
        private readonly IMenuItem menuItemService;
        /// <summary>
        /// Initialize category and manu services in constructor
        /// </summary>
        /// <param name="_category"></param>
        /// <param name="_menuItem"></param>
        public MenuItemController(ICategory _category, IMenuItem _menuItem)
        {
            categoryServise = _category;
            menuItemService = _menuItem;
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
        public async Task<IActionResult> Add()
        {
            var menu = new AddMenuItemViewModel()
            {
                Categories = await categoryServise.GetAllCategoriesAsync()
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

            if (await menuItemService.HasThisEntityAsync(model.Name))
            {
                TempData["Error"] = "Alredy has entity with this name. Try with the other one !";

                return RedirectToAction("Add");
            }

            await menuItemService.AddMenuItemAsync(model);

            return RedirectToAction("All");
        }
        /// <summary>
        /// Show all MenuItems
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var allMemuItem = new MultipleMenuItemViewModel
            {
                ActiveMenuItems = await menuItemService.GetAllMenuItemsAsync(),
                DeletedMenuItems = await menuItemService.GetAllDeletedMenuItemsAsync()
            };

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
            var model = await menuItemService.EditGetMenuItemAsync(id);

            model.Categories = await categoryServise.GetAllCategoriesAsync(); 

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

            await menuItemService.EditPostMenuItemAsync(model);

            return RedirectToAction("All");
        }
        /// <summary>
        /// Delete MenuItem
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int Id)
        {
            await menuItemService.DeleteMenuItemAsync(Id);

            return RedirectToAction("All");
        }

        /// <summary>
        /// Restore MenuItem
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Restore(int Id)
        {
            await menuItemService.RestoreMenuItemAsync(Id);

            return RedirectToAction("All");
        }

    }
}
