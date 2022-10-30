using RestaurantManagmentSystem.Core.Models.MenuItems;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface IMenuItem
    {
        Task AddMenuItemAsync(AddMenuItemViewModel model);
        IEnumerable<MenuItemViewModel> GetAllMenuItems();
        Task EditPostMenuItemAsync(EditMenuItemViewModel model);
        Task<EditMenuItemViewModel> EditGetMenuItemAsync(int Id);
         Task DeleteMenuItemAsync(int Id);
        Task<EditMenuItemViewModel> GetByIdMenuItem(int Id);
    }
}
