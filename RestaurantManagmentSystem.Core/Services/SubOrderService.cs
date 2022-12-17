using Microsoft.EntityFrameworkCore;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Categories;
using RestaurantManagmentSystem.Core.Models.SubOrder;
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

        public async Task AddCategoriesToSubOrderAsync(SubOrderViewModel model)
        {
            var category = model.Categories.Where(x => x.MenuItems.Any(x => x.IsChecked == true)).ToList();

            var subOrder = await repo.AllReadonly<SubOrder>().Where(x => x.Id == model.Id).FirstAsync();


            subOrder.Categories = model.Categories
                .Where(x => x.MenuItems.Any(x => x.IsChecked == true))
                .Select(x => new Category()
            {
                Id = x.Id,
                Name = x.Name,
                IsChecked = x.IsChecked,
                IsDeleted = x.IsDeleted,
                MenuItems = x.MenuItems.Select(x => new MenuItem()
                {
                    Id = x.Id,
                    Price = x.Price,
                    Name = x.Name,
                    CategoryId = x.CategoryId,
                    IsChecked= x.IsChecked,
                    IsDeleted= x.IsDeleted,
                    ItemsForCooking = x.ItemsForCooking
                    
                }).ToList()
            }).ToList();

            await repo.SaveChangesAsync();



            foreach (var cat in category)
            {
                var catId = cat.Id;

                subOrder.CategoryId = catId;

                await repo.SaveChangesAsync();

                foreach (var item in cat.MenuItems)
                {
                    var itemId = item.Id;

                    subOrder.ProductId = itemId;

                    await repo.SaveChangesAsync();
                }
            }


            decimal currentTotalSum = 0;

            foreach (var cat in category)
            {
                var curentsum = cat.MenuItems.Sum(x => x.Price);

                currentTotalSum += curentsum;
            }

            subOrder.CurrentTotalSum = currentTotalSum;

            await repo.SaveChangesAsync();
        }

        public async Task<SubOrder> AddOrderedItemsAsync(IEnumerable<Category> model, int Id)
        {
            var subOrd = await repo.All<SubOrder>().Where(x => x.Id == Id)
               .SingleOrDefaultAsync();

            return subOrd;
        }

        public async Task<int> CreateSubOrderAsync(SubOrderViewModel model, int Id)
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

        public async Task<SubOrderViewModel> GetSubOrderByIdAsync(int Id)
        {
            var subOrd = await repo.All<SubOrder>()
                .Where(x => x.Id == Id)
                .Select(x => new SubOrderViewModel()
                {
                    Id = x.Id,
                    IsCompleted = x.IsCompleted,
                    IsDeleted = x.IsDeleted,
                    CreateOn = x.CreateOn,
                    CompletedOn = x.CompletedOn,
                    CurrentTotalSum = x.CurrentTotalSum,
                    OrderId = x.OrderId
                })
                .FirstAsync();

            if (subOrd == null)
            {
                throw new ArgumentNullException();
            }

            return subOrd;

        }
    }
}
