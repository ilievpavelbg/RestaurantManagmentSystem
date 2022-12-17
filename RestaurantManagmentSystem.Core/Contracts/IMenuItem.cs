using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.MenuItems;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface IMenuItem
    {
        Task AddMenuItemAsync(AddMenuItemViewModel model);
        Task<IEnumerable<EditMenuItemViewModel>> GetAllMenuItemsAsync();
        Task<IEnumerable<EditMenuItemViewModel>> GetAllDeletedMenuItemsAsync();
        Task EditPostMenuItemAsync(EditMenuItemViewModel model);
        Task<EditMenuItemViewModel> EditGetMenuItemAsync(int Id);
         Task DeleteMenuItemAsync(int Id);
        Task<EditMenuItemViewModel> GetByIdMenuItem(int Id);
        Task<bool> HasThisEntityAsync(string name);
        Task RestoreMenuItemAsync(int Id);
        Task<IEnumerable<TempOrderMenuItemViewModel>> GetAllMenuItemsTempOrderAsync();
        Task<List<MenuItem>> GetAllSubOrderMenuItemsAsync();
    }
}
