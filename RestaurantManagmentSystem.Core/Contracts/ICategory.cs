using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Categories;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface ICategory
    {
        Task AddCategoryAsync(CategoryViewModel model);
        bool HasThisEntity(CategoryViewModel model);
        IEnumerable<Category> GetAllCategories();
        Task DeleteCategoryAsync(int Id);
        Task EditPostCategoryAsync(EditCategoryViewModel model);
        Task<EditCategoryViewModel> EditGetCategoryAsync(int Id);
        Task<CategoryViewModel> GetCategoryById(int Id);
    }
}
