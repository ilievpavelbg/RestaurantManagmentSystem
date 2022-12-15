using RestaurantManagmentSystem.Core.Data;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface ISubOrder
    {
        Task<SubOrder> CreateSubOrderAsync(int Id);

        Task<SubOrder> AddOrderedItemsAsync(IEnumerable<Category> model, int Id);

        Task<SubOrder> GetSubOrderByIdAsync(int Id);

        Task<SubOrder> AddCategoriesToSubOrderAsync(IEnumerable<Category> model);
    }
}
