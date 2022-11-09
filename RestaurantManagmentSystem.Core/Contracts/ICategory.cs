using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Categories;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface ICategory
    {
        Task AddCategoryAsync(CategoryViewModel model);
        bool HasThisEntity(string name);
        Task<IEnumerable<EditCategoryViewModel>> GetAllCategoriesAsync();
        Task<IEnumerable<EditCategoryViewModel>> GetAllDeletedCategoriesAsync();
        Task DeleteCategoryAsync(int Id);
        Task RestoreCategoryAsync(int Id);
        Task EditPostCategoryAsync(EditCategoryViewModel model);
        Task<EditCategoryViewModel> EditGetCategoryAsync(int Id);
        Task<CategoryViewModel> GetCategoryById(int Id);
    }
}
