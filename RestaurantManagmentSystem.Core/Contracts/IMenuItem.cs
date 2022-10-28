using RestaurantManagmentSystem.Core.Models;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface IMenuItem
    {
        Task AddMenuItemAsync(AddMenuItemViewModel model);
        IEnumerable<MenuItemViewModel> GetAllMenuItems();
        Task EditMenuItemAsync(EditMenuItemViewModel model);
        Task<EditMenuItemViewModel> EditMenuItemViewAsync(int Id);
         Task DeleteMenuItemAsync(int Id);
    }
}
