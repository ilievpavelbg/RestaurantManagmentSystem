using RestaurantManagmentSystem.Core.Models.Orders;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface IOrder
    {
        Task<OrderViewModel> CreateOrderAsync(int employeeId, int tableId);
        Task<OrderViewModel> GetOrderByIdAsync(int Id);
        bool GetOrderIdByTableId(int Id);
    }
}
