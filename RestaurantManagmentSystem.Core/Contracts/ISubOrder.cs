using RestaurantManagmentSystem.Core.Models.SubOrder;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface ISubOrder
    {
        Task<SubOrderViewModel> CreateSubOrderAsync(int Id);
    }
}
