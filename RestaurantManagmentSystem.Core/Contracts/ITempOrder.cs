using RestaurantManagmentSystem.Core.Data;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface ITempOrder
    {
        Task AddMenuItemsToTempOrderAsync(IEnumerable<TempOrderMenuItemViewModel> items, int Id);
        Task<int> CreateTempOrderAsync(TempOrder model, int Id);
    }
}
