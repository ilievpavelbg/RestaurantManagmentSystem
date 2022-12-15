using Microsoft.EntityFrameworkCore;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Categories;
using RestaurantManagmentSystem.Core.Models.MenuItems;
using RestaurantManagmentSystem.Core.Repository.Common;

namespace RestaurantManagmentSystem.Core.Services
{
    public class CategoryService : ICategory
    {
        private readonly IRepository repo;
        /// <summary>
        /// Initialize Repo in constructor
        /// </summary>
        /// <param name="_repo"></param>
        public CategoryService(IRepository _repo)
        {
            repo = _repo;
        }
        /// <summary>
        /// Adding new Category to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateCategoryAsync(CategoryViewModel model)
        {
            var category = new Category()
            {
                Name = model.Name

            };

            await repo.AddAsync<Category>(category);
            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Get the selected Category and render it to Edit page
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<EditCategoryViewModel> EditGetCategoryAsync(int Id)
        {
            var category = await repo.GetByIdAsync<Category>(Id);

            if (category == null)
            {
                throw new ArgumentException("Category with this ID can not be found !");

            }

            var model = new EditCategoryViewModel()
            {
                Name = category.Name
            };

            return model;

        }
        /// <summary>
        /// Get the selected EditCategoryViewModel from HHTPGet Edit, and save changes to DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditPostCategoryAsync(EditCategoryViewModel model)
        {
            var category = await repo.GetByIdAsync<Category>(model.Id);

            category.Name = model.Name;

            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Get all categories when created
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EditCategoryViewModel>> GetAllCategoriesAsync()
        {
            var allCat = await repo.All<Category>()
                .Where(x => x.IsDeleted == false)
                .Select(vm => new EditCategoryViewModel
                {
                    Id = vm.Id,
                    Name = vm.Name
                })
                .ToListAsync();

            return allCat;
        }
        /// <summary>
        /// Get all categories for the SubOrderModel
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> GetAllCategoriesSubOrderAsync()
        {

            var items =  repo.AllReadonly<MenuItem>();

            var allCat = await repo.All<Category>()
                .Where(x => x.IsDeleted == false)
                .ToListAsync();

          
            return allCat;
        }
        /// <summary>
        /// Check if in databes already exixt such entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> HasThisEntityAsync(string name)
        {
            return await repo.All<Category>().AnyAsync(c => c.Name == name);
        }
        /// <summary>
        /// Delete Category as put the IsDeleted property to true, no physical detetion from DB
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteCategoryAsync(int Id)
        {
            var menuItems = repo.All<MenuItem>(x => x.CategoryId == Id && x.IsDeleted == false);

            var category = await repo.GetByIdAsync<Category>(Id);

            if (menuItems.Any())
            {
                throw new ArgumentException($"First have to delete all MenuItems with category {category.Name}!");
            }

            category.IsDeleted = true;

            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Get category by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CategoryViewModel> GetCategoryById(int Id)
        {
            var category = await repo.GetByIdAsync<Category>(Id);

            var model = new CategoryViewModel()
            {
                Name = category.Name
            };

            return model;
        }
        /// <summary>
        /// Restore Category as put the IsDeleted property to false
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task RestoreCategoryAsync(int Id)
        {
            var category = await repo.GetByIdAsync<Category>(Id);

            category.IsDeleted = false;

            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Get All Deleted Categories
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EditCategoryViewModel>> GetAllDeletedCategoriesAsync()
        {
            var allCat = await repo.All<Category>()
                .Where(x => x.IsDeleted == true)
                .Select(vm => new EditCategoryViewModel
                {
                    Id = vm.Id,
                    Name = vm.Name
                })
                .ToListAsync();

            return allCat;
        }

        public async Task<IEnumerable<Category>> AddMenuItemsToCategory(List<MenuItem> item, IEnumerable<Category> category)
        {
            foreach (var cat in category)
            {

                cat.MenuItems = item.Where(x => x.CategoryId == cat.Id).ToList();   

            }

            await repo.SaveChangesAsync();

            return category;
        }
    }
}
