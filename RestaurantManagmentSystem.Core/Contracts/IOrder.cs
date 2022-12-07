using RestaurantManagmentSystem.Core.Models.Orders;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface IOrder
    {
        Task CreateOrderAsync(OrderViewModel model);
    }
}
