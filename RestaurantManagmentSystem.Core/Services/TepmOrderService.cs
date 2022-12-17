using Microsoft.EntityFrameworkCore;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Repository.Common;

namespace RestaurantManagmentSystem.Core.Services
{
    public class TepmOrderService : ITempOrder
    {
        private readonly IRepository repo;
        /// <summary>
        /// Initialize Repo in constructor
        /// </summary>
        /// <param name="_repo"></param>
        public TepmOrderService(IRepository _repo)
        {
            repo = _repo;
        }
        public async Task AddMenuItemsToTempOrderAsync(IEnumerable<TempOrderMenuItemViewModel> items, int Id)
        {
            var order = await repo.All<TempOrder>().Where(x => x.Id == Id).FirstAsync();

            order.MenuItems = items;

            await repo.AddAsync<TempOrder>(order);

            foreach (var menuItem in order.MenuItems)
            {
                order.ItemName = menuItem.Name;
                order.Price = menuItem.Price;
                order.ItemsForCooking = menuItem.ItemsForCooking;
                order.IsChecked = menuItem.IsChecked;
               
                await repo.SaveChangesAsync();
            }


            await repo.SaveChangesAsync();
        }

        public async Task<int> CreateTempOrderAsync(TempOrder model, int Id)
        {

            var TOrder = new TempOrder()
            {
                OrderId = Id,
            };

            await repo.AddAsync<TempOrder>(TOrder);
            await repo.SaveChangesAsync();

            return TOrder.Id;
        }
    }
}
