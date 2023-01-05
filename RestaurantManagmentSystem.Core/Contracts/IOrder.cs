using RestaurantManagmentSystem.Core.Data;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface IOrder
    {
        Task<Order> CreateOrderAsync(int employeeId, int tableId);
        Task<Order> GetOrderByIdAsync(int Id);
        bool GetOrderIdByTableId(int Id);

        Task CloseTheOrder(int Id);
    }
}
