using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Models.Category;
using RestaurantManagmentSystem.Core.Repository.Common;
using RestaurantOrderManagmentSystem.Core.Data;

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

            await repo.AddAsync(category);
            await repo.SaveChangesAsync();
        }

        public IEnumerable<Category> GetAllCategoriesAsync()
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

        public Task RemoveCategoryAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategory(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
