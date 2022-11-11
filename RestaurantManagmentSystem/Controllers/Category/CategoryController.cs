using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Models.Categories;

namespace RestaurantManagmentSystem.Controllers.Category
{
    public class CategoryController : Controller
    {
        private readonly ICategory categoryService;
        /// <summary>
        /// Initialize category service
        /// </summary>
        /// <param name="_categoryService"></param>
        public CategoryController(ICategory _categoryService)
        {
            categoryService = _categoryService;
        }
        /// <summary>
        /// Category Index page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return RedirectToAction("All");
        }
        /// <summary>
        /// Add Category View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new MultipleCategoryViewModel();

            model.CategoryModel = new CategoryViewModel();
            model.ActiveCategories = await categoryService.GetAllCategoriesAsync();
            model.DeletedCategories = await categoryService.GetAllDeletedCategoriesAsync();

            return View(model);
        }
        /// <summary>
        /// Add Category to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(MultipleCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            if (await categoryService.HasThisEntityAsync(model.CategoryModel.Name))
            {
                TempData["Error"] = "Alredy has entity with this name. Try with the other one !";

                return RedirectToAction("Add");
            }

            await categoryService.AddCategoryAsync(model.CategoryModel);

            return RedirectToAction("Add");
        }
        /// <summary>
        /// Show all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var allCategories = await categoryService.GetAllCategoriesAsync();

            return View(allCategories);
        }
        /// <summary>
        /// Edit Category View
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var category = await categoryService.EditGetCategoryAsync(Id);

            return View(category);
        }
        /// <summary>
        /// Edit Category, update database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await categoryService.EditPostCategoryAsync(model);

            return RedirectToAction("Add");
        }
        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int Id)
        {
            var category = await categoryService.GetCategoryById(Id);

            try
            {
                await categoryService.DeleteCategoryAsync(Id);

                TempData["message"] = $"Succesfully deleted {category.Name}";

                return RedirectToAction("Add");
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return RedirectToAction("Add");
            }

        }
        /// <summary>
        /// Restore Category
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Restore(int Id)
        {
            await categoryService.RestoreCategoryAsync(Id);

            return RedirectToAction("Add");
        }
    }
}
