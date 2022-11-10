﻿using RestaurantManagmentSystem.Core.Models.Categories;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface ICategory
    {
        Task AddCategoryAsync(CategoryViewModel model);
        Task<bool> HasThisEntityAsync(string name);
        Task<IEnumerable<EditCategoryViewModel>> GetAllCategoriesAsync();
        Task<IEnumerable<EditCategoryViewModel>> GetAllDeletedCategoriesAsync();
        Task DeleteCategoryAsync(int Id);
        Task RestoreCategoryAsync(int Id);
        Task EditPostCategoryAsync(EditCategoryViewModel model);
        Task<EditCategoryViewModel> EditGetCategoryAsync(int Id);
        Task<CategoryViewModel> GetCategoryById(int Id);
    }
}
