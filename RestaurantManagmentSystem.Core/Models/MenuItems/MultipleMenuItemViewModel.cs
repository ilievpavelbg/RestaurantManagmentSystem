namespace RestaurantManagmentSystem.Core.Models.MenuItems
{
    public class MultipleMenuItemViewModel
    {
        public IEnumerable<EditMenuItemViewModel> ActiveMenuItems { get; set; } = new List<EditMenuItemViewModel>();
        public IEnumerable<EditMenuItemViewModel> DeletedMenuItems { get; set; } = new List<EditMenuItemViewModel>();
    }
}
