using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Common;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Categories;
using RestaurantManagmentSystem.Core.Models.Orders;
using RestaurantManagmentSystem.Core.Models.SubOrder;
using RestaurantManagmentSystem.Core.Repository.Common;

namespace RestaurantManagmentSystem.Areas.Waiter.Controllers
{
    [Area("Waiter")]
    public class OrderController : Controller
    {
        private readonly IOrder orderServises;
        private readonly ISubOrder subOrderServises;
        private readonly ITable_1 tableService;
        private readonly IRepository repo;
        private readonly ICategory categoryService;
        private readonly IMenuItem menuItemService;
        public OrderController(
            IOrder _orderServises, 
            IRepository _repo,
            ITable_1 _tableService,
            ISubOrder _subOrderServises,
            ICategory _categoryService,
            IMenuItem _menuItemService
            )
        {
            orderServises = _orderServises;
            repo = _repo;
            tableService = _tableService;
            subOrderServises = _subOrderServises;
            categoryService = _categoryService;
            menuItemService = _menuItemService;
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

            await tableService.SaveCurrentOrderIdToTable(model.OrderId, Id);

            return RedirectToAction("Details", new { id = model.OrderId });
        }

        public async Task<IActionResult> Details(int Id)
        {
            var model = await orderServises.GetOrderByIdAsync(Id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Purchase()
        {
            var model = new SubOrderViewModel()
            {
                Categories = await categoryService.GetAllCategoriesSubOrderAsync()
            };
           


            return View(model);
        }

        [HttpPost]
        public IActionResult Purchase(SubOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Console.WriteLine("Hello");
            //var model = await subOrderServises.CreateSubOrderAsync();

            return RedirectToAction("Details");
        }

        public async Task<IActionResult> CreateSubOrder(int Id)
        {
           await subOrderServises.CreateSubOrderAsync(Id);

            return RedirectToAction("Purchase");
        }
    }
}
