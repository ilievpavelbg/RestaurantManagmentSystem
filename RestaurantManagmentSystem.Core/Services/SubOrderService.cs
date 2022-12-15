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

        public async Task<SubOrder> AddOrderedItemsAsync(IEnumerable<Category> model, int Id)
        {
            var subOrd = await repo.All<SubOrder>().Where(x => x.Id == Id)
               .SingleOrDefaultAsync();

            return subOrd;
        }

        public async Task<SubOrder> CreateSubOrderAsync(int Id)
        {

            var subOrder = new SubOrder()
            {
                IsCompleted = false,
                IsDeleted = false,
                CreateOn = DateTime.Now,
                CompletedOn = null,
                CurrentTotalSum = 0,
                OrderId = Id
            };

            await repo.AddAsync<SubOrder>(subOrder);
            await repo.SaveChangesAsync();


            return subOrder;
        }

        public async Task<SubOrder> GetSubOrderByIdAsync(int Id)
        {
            return await repo.GetByIdAsync<SubOrder>(Id);

        }
    }
}
