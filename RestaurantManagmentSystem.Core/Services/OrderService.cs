using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Data.Enum;
using RestaurantManagmentSystem.Core.Models.Orders;
using RestaurantManagmentSystem.Core.Repository.Common;

namespace RestaurantManagmentSystem.Core.Services
{
    public class OrderService : IOrder
    {
        private readonly IRepository repo;
        /// <summary>
        /// Initialize Repo in constructor
        /// </summary>
        /// <param name="_repo"></param>
        public OrderService(IRepository _repo)
        {
            repo = _repo;
        }
        public async Task CreateOrderAsync(OrderViewModel model)
        {

            var order = new Order()
            {
                CreatedOn = DateTime.Now,
                OrderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus),model.OrderStatus),
                EmployeeId = model.EmployeeId,
                TableId = model.TableId,
                TotalPrice = model.TotalPrice,
               

            };

            await repo.AddAsync<Order>(order);
            await repo.SaveChangesAsync();
        }
    }
}
