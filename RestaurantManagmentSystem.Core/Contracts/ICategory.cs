using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Categories;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface ICategory
    {
        Task AddCategoryAsync(CategoryViewModel model);
        bool HasThisEntity(CategoryViewModel model);
        IEnumerable<Category> GetAllCategories();
        //Task RemoveCategoryAsync(int Id);
        //Task UpdateCategory(int Id);
    }
}
