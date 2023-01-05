using Microsoft.EntityFrameworkCore;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Repository.Common;

namespace RestaurantManagmentSystem.Core.Services
{
    public class SubOrderService : ISubOrder
    {
        private readonly IRepository repo;
        /// <summary>
        /// Initialize Repo in constructor
        /// </summary>
        /// <param name="_repo"></param>
        public SubOrderService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<int> AddCategoriesToSubOrderAsync(IEnumerable<Category> model, int Id)
        {
            var categories = new List<Category>();

            foreach (var category in model)
            {
                if (category.MenuItems.Any(x => x.IsChecked == true))
                {
                    var checkedItemsOnly = category.MenuItems.Where(x => x.IsChecked == true).ToList();
                    category.MenuItems = checkedItemsOnly;

                    categories.Add(category);
                }

            }


            var subOrder = await repo.GetByIdAsync<SubOrder>(Id);

            var currentTotal =
                model.Sum(x => x.MenuItems.Where(mi => mi.IsChecked == true && mi.OrderedQty > 0)
            .Sum(mi => (mi.Price * mi.OrderedQty)));

            subOrder.Categories = categories;
            subOrder.CreateOn = DateTime.Now;
            subOrder.CurrentTotalSum = currentTotal ?? 0;

            var order = await repo.GetByIdAsync<Order>(subOrder.OrderId);

            order.SubOrders.ToList().Add(subOrder);


            await repo.SaveChangesAsync();

            return order.Id;

        }

        public async Task<SubOrder> AddOrderedItemsAsync(IEnumerable<Category> model, int Id)
        {
            var subOrd = await repo.All<SubOrder>().Where(x => x.Id == Id)
               .SingleOrDefaultAsync();

            return subOrd;
        }

        public async Task<int> CreateSubOrderAsync(SubOrder model, int Id)
        {

            var subOrder = new SubOrder()
            {
                IsCompleted = model.IsCompleted,
                IsDeleted = model.IsDeleted,
                CreateOn = model.CreateOn,
                CompletedOn = model.CompletedOn,
                CurrentTotalSum = 0,
                OrderId = Id
            };

            await repo.AddAsync<SubOrder>(subOrder);
            await repo.SaveChangesAsync();

            return subOrder.Id;
        }

        public async Task AddSubOrderToOrderAsync(int Id)
        {
            var subOrders = await repo.All<SubOrder>().Where(x => x.OrderId == Id).ToListAsync();

            var order = await repo.All<Order>().Where(x => x.Id == Id).FirstAsync();

            order.SubOrders = repo.All<SubOrder>().Where(x => x.OrderId == order.Id);

            await repo.SaveChangesAsync();

        }

        public async Task<SubOrder> GetSubOrderByIdAsync(int Id)
        {
            var subOrd = await repo.GetByIdAsync<SubOrder>(Id);

            if (subOrd == null)
            {
                throw new ArgumentNullException();
            }

            return subOrd;

        }
    }
}
