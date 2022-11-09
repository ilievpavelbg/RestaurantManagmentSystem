namespace RestaurantManagmentSystem.Core.Models.Categories
{
    public class MultipleCategoryViewModel
    {
        public CategoryViewModel CategoryModel { get; set; } = null!;
        public IEnumerable<EditCategoryViewModel> ActiveCategories { get; set; } = new List<EditCategoryViewModel>();
        public IEnumerable<EditCategoryViewModel> DeletedCategories { get; set; } = new List<EditCategoryViewModel>();

    }
}
