using RestaurantManagmentSystem.Core.Models.MenuItems;

namespace RestaurantManagmentSystem.Core.Models.Categories
{
    public class SubOrderCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsChecked { get; set; }

        public List<OrderMenuItemViewModel> MenuItems { get; set; } = new List<OrderMenuItemViewModel>();
    }
}
