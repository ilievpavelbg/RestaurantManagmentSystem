using Microsoft.EntityFrameworkCore;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Data.Enum;
using RestaurantManagmentSystem.Core.Models.Orders;
using RestaurantManagmentSystem.Core.Models.SubOrder;
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
        public async Task<OrderViewModel> CreateOrderAsync(int employeeId, int tableId)
        {

            var order = new Order()
            {
                CreatedOn = DateTime.Now,
                OrderStatus = OrderStatus.Active,
                EmployeeId = employeeId,
                TableId = tableId,
                TotalPrice = 0,
            };

            await repo.AddAsync<Order>(order);
            await repo.SaveChangesAsync();

            var model = new OrderViewModel
            {
                OrderId = order.Id,
                CreatedOn = order.CreatedOn,
                EmployeeId = order.EmployeeId,
                TableId = order.TableId,
                TotalPrice = order.TotalPrice
            };

            return model;
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(int orderId)
        {
            var subOrders = await repo.All<SubOrder>()
                .Where(x => x.OrderId == orderId)
                .Select(so => new SubOrderViewModel()
                {
                    OrderId = so.OrderId,
                    CreateOn = so.CreateOn,
                    CurrentTotalSum = so.CurrentTotalSum,

                }).ToListAsync();
           
                var order = await repo.All<Order>().Where(x => x.Id == orderId)
                .Select(or => new OrderViewModel()
                {
                    OrderId = or.Id,
                    CreatedOn = or.CreatedOn,
                    EmployeeId = or.EmployeeId,
                    TableId = or.TableId,
                    TotalPrice = or.TotalPrice,
                    SubOrders = subOrders
                })
                .FirstOrDefaultAsync();

            return order;
        }
    }
}
