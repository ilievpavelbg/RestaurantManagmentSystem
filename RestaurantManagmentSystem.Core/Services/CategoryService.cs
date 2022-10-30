using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Categories;
using RestaurantManagmentSystem.Core.Repository.Common;

namespace RestaurantManagmentSystem.Core.Services
{
    public class CategoryService : ICategory
    {
        private readonly IRepository repo;

        public CategoryService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddCategoryAsync(CategoryViewModel model)
        {
            var category = new Category()
            {
                Name = model.Name
            };

            await repo.AddAsync<Category>(category);
            await repo.SaveChangesAsync();
        }

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

        public async Task EditPostCategoryAsync(EditCategoryViewModel model)
        {
            var category = await repo.GetByIdAsync<Category>(model.Id);

            category.Name = model.Name;

            await repo.SaveChangesAsync();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var allCat = repo.All<Category>().ToList();

            return allCat;
        }

        public bool HasThisEntity(CategoryViewModel model)
        {
            var entity = repo.All<Category>(x => x.Name == model.Name).FirstOrDefault();

            if (entity != null)
            {
                return true;
            }

            return false;
        }

        public async Task DeleteCategoryAsync(int Id)
        {
            var category = await repo.GetByIdAsync<Category>(Id);

            category.IsDeleted = true;

            repo.Update(category);
        }

        
    }
}
