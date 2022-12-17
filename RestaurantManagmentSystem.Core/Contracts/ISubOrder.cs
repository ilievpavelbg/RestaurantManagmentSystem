using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.SubOrder;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface ISubOrder
    {
        Task<int> CreateSubOrderAsync(SubOrderViewModel model, int Id);

        Task<SubOrder> AddOrderedItemsAsync(IEnumerable<Category> model, int Id);

        Task<SubOrderViewModel> GetSubOrderByIdAsync(int Id);
        Task AddSubOrderToOrderAsync(int Id);

        Task AddCategoriesToSubOrderAsync(SubOrderViewModel model);
    }
}
