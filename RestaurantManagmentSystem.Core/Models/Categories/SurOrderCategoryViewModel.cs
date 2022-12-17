using RestaurantManagmentSystem.Core.Models.MenuItems;

namespace RestaurantManagmentSystem.Core.Models.Categories
{
    public class SubOrderCategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsChecked { get; set; }
        public bool IsDeleted { get; set; }

        public int? SubOrderId { get; set; }

        public List<SubOrderMenuItemsViewModel> MenuItems { get; set; } = new List<SubOrderMenuItemsViewModel>();
    }
}
