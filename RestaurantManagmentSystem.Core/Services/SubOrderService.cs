using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
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
        public async Task<SubOrderViewModel> CreateSubOrderAsync(int Id)
        {

            var subOrder = new SubOrder()
            {
                IsCompleted = false,
                CreateOn = DateTime.Now,
                CurrentTotalSum = 0,
                OrderId = Id
               
            };

            await repo.AddAsync<SubOrder>(subOrder);
            await repo.SaveChangesAsync();

            var sub = new SubOrderViewModel()
            {
                IsCompleted = false,
                CreateOn = DateTime.Now,
                CurrentTotalSum = 0
                
            };

            return sub;
        }
    }
}
