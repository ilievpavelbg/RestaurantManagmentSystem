using Microsoft.EntityFrameworkCore;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Data.Enum;
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
        public async Task<Order> CreateOrderAsync(int employeeId, int tableId)
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

            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var subOrders = await repo.All<SubOrder>()
                .Where(x => x.OrderId == orderId)
                .ToListAsync();
           
                var order = await repo.All<Order>().Where(x => x.Id == orderId)
                .FirstOrDefaultAsync();

            order.SubOrders = subOrders;

            await repo.SaveChangesAsync();

            return order;
        }

        public bool GetOrderIdByTableId(int Id)
        {
            var hasOrder = repo.All<Order>().Any(x => x.TableId == Id && x.IsDeleted == false);

            //var model = repo.AllReadonly<Order>()
            //    .Where(x => x.TableId == Id && x.IsDeleted == false)
            //    .SingleOrDefaultAsync();

            //var orderId = model.Id;

            return hasOrder;
        }
    }
}
