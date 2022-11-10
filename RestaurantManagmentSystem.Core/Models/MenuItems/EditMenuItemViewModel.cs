namespace RestaurantManagmentSystem.Core.Models.MenuItems
{

    public class EditMenuItemViewModel : AddMenuItemViewModel
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
