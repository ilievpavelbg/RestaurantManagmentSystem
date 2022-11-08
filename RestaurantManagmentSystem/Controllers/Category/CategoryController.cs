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
        public IActionResult Add()
        {
            var model = new CategoryViewModel();

            var allCategory = categoryService.GetAllCategories();

            ViewBag.data = allCategory;

            return View(model);
        }
        /// <summary>
        /// Add Category to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (categoryService.HasThisEntity(model))
            {
                ModelState.AddModelError("", "Alredy has entity with this name !");
                return View(model);
            }

            await categoryService.AddCategoryAsync(model);

            //return View(model);
            return RedirectToAction("Add");
        }
        /// <summary>
        /// Show all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult All()
        {
            var allCategories = categoryService.GetAllCategories();

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

            return RedirectToAction("All");
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

                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return RedirectToAction("All");
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
