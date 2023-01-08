using RestaurantManagmentSystem.Core.Data;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface ISubOrder
    {
        Task<int> CreateSubOrderAsync(SubOrder model, int Id);
        Task<SubOrder> GetSubOrderByIdAsync(int Id);
        Task AddSubOrderToOrderAsync(int Id);
        Task<int> AddCategoriesToSubOrderAsync(IEnumerable<Category> model, int Id);
        Task<IEnumerable<SubOrder>> GetAllSubOrdersChef();
        Task CompleteSubOrder(int Id);

        Task<bool> AllSubOrdersAreNotCompleted(int Id);
    }
}
