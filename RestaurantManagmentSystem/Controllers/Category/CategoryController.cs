using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Models.Categories;

namespace RestaurantManagmentSystem.Controllers.Category
{
    public class CategoryController : Controller
    {
        private readonly ICategory categoryService;

        public CategoryController(ICategory _categoryService)
        {
            categoryService = _categoryService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new CategoryViewModel();

            return View(model);
        }

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

            return RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult All()
        {
            var allCategories = categoryService.GetAllCategories();

            return View(allCategories);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var category = await categoryService.EditGetCategoryAsync(Id);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryViewModel model)
        {
           await categoryService.EditPostCategoryAsync(model);

            return RedirectToAction("All");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await categoryService.DeleteCategoryAsync(Id);

            return RedirectToAction("All");
        }
    }
}
