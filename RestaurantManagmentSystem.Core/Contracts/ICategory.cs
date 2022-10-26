using RestaurantManagmentSystem.Core.Models.Category;
using RestaurantOrderManagmentSystem.Core.Data;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface ICategory
    {
        Task AddCategoryAsync(CategoryViewModel model);
        bool HasThisEntity(CategoryViewModel model);

        IEnumerable<Category> GetAllCategoriesAsync();
        //Task RemoveCategoryAsync(int Id);
        //Task UpdateCategory(int Id);
    }
}
