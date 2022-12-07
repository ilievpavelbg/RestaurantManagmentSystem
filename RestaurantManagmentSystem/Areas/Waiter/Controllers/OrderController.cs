using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Common;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Data.Enum;
using RestaurantManagmentSystem.Core.Models.MenuItems;
using RestaurantManagmentSystem.Core.Models.Orders;
using RestaurantManagmentSystem.Core.Repository.Common;
using RestaurantManagmentSystem.Core.Services;

namespace RestaurantManagmentSystem.Areas.Waiter.Controllers
{
    [Area("Waiter")]
    public class OrderController : Controller
    {
        private readonly IOrder orderServises;
        private readonly IRepository repo;
        public OrderController(IOrder _orderServises, IRepository _repo)
        {
            orderServises = _orderServises;
            repo = _repo;
        }

        public IActionResult Create(int Id)
        {
            var userId = User.Id();

            var emplId = repo.All<Employee>().Where(x => x.ApplicationUserId == userId).FirstOrDefault();


            var order = new OrderViewModel()
            {
                EmployeeId = emplId.Id,
                TableId = Id,
                OrderStatus = OrderStatus.Active.ToString(),
                CreatedOn = DateTime.Now
            };

            return View(order);
        }
    }
}
