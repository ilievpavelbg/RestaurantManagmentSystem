using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Categories;
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
        public async Task AddCategoryAsync(CategoryViewModel model)
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

            var model = new EditCategoryViewModel()
            {
                Id = Id,
                Name = category.Name
            };

            return model;

        }
        /// <summary>
        /// Get the chosed MenuItemViewModel from HHTPGet Edit, and save changes to DB
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
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetAllCategories()
        {
            var allCat = repo.All<Category>().ToList();

            return allCat;
        }
        /// <summary>
        /// Check if in databes already exixt such entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool HasThisEntity(CategoryViewModel model)
        {
            var entity = repo.All<Category>(x => x.Name == model.Name).FirstOrDefault();

            if (entity != null)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Delete MenuItem as put the IsDeleted property to true, no physical detetion from DB
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteCategoryAsync(int Id)
        {
            var menuItems = repo.All<MenuItem>(x => x.CategoryId == Id && x.IsDeleted == false);

            if (menuItems.Any())
            {
                throw new ArgumentException("Have to delete all menuItems included in this category!");
            }
            
            var category = await repo.GetByIdAsync<Category>(Id);

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
    }
}
