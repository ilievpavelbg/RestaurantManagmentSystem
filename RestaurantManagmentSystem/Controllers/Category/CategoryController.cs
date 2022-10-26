using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Models.Category;

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
            return View();
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
            var allCategories = categoryService.GetAllCategoriesAsync();

            return View(allCategories);
        }

        
    }
}
