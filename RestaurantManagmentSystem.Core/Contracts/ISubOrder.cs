using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.SubOrder;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface ISubOrder
    {
        Task<int> CreateSubOrderAsync(SubOrder model, int Id);

        Task<SubOrder> AddOrderedItemsAsync(IEnumerable<Category> model, int Id);

        Task<SubOrder> GetSubOrderByIdAsync(int Id);
        Task AddSubOrderToOrderAsync(int Id);

        Task AddCategoriesToSubOrderAsync(IEnumerable<Category> model, int Id);
    }
}
