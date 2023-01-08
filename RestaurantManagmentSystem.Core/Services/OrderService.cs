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

        public async Task CloseTheOrder(int Id)
        {
            var order = await repo.GetByIdAsync<Order>(Id);

            order.IsDeleted = true;
            order.ClosedOn = DateTime.Now;

            var table = await repo.GetByIdAsync<Table>(order.TableId);

            table.IsReserved = false;
            table.OrderId = null;

            await repo.SaveChangesAsync();

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
            var subOrd = repo.All<SubOrder>().Where(x => x.OrderId == orderId).ToList();

            foreach (var sub in subOrd)
            {
                sub.Categories =  repo.All<Category>().Where(x => x.SubOrderId == sub.Id).ToList();

                foreach (var categ in sub.Categories)
                {
                    categ.MenuItems = repo.All<MenuItem>().Where(x => x.CategoryId == categ.Id).ToList();
                }
            }

            var order = await repo.GetByIdAsync<Order>(orderId);

            order.SubOrders = subOrd;

            return order;
        }

        public bool GetOrderIdByTableId(int Id)
        {
            var hasOrder = repo.All<Order>().Any(x => x.TableId == Id && x.IsDeleted == false);

            return hasOrder;
        }
    }
}
