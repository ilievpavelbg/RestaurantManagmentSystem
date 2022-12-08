using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Common;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Data.Enum;
using RestaurantManagmentSystem.Core.Models.Orders;
using RestaurantManagmentSystem.Core.Repository.Common;

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

        public async Task<IActionResult> Create(int Id)
        {
            var userId = User.Id();

            var emplId =  repo.All<Employee>().Where(x => x.ApplicationUserId == userId).FirstOrDefault();

            if (emplId == null)
            {
                throw new ArgumentNullException();
            }

            var model = await orderServises.CreateOrderAsync(emplId.Id, Id);

            return RedirectToAction("Details", new { id = model.OrderId });
        }

        public async Task<IActionResult> Details(int Id)
        {
            var model = await orderServises.GetOrderByIdAsync(Id);

            return View(model);
        }
    }
}
