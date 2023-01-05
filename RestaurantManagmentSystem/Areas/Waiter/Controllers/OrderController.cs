using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Common;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Repository.Common;

namespace RestaurantManagmentSystem.Areas.Waiter.Controllers
{
    [Area("Waiter")]
    [Authorize(Roles = "Waiter, Manager")]
    public class OrderController : Controller
    {
        private readonly IOrder orderServises;
        private readonly ISubOrder subOrderServises;
        private readonly ITable_1 tableService;
        private readonly IRepository repo;
        private readonly ICategory categoryService;
        private readonly IMenuItem menuService;

        public OrderController(
            IOrder _orderServises, 
            IRepository _repo,
            ITable_1 _tableService,
            ISubOrder _subOrderServises,
            ICategory _categoryService,
            IMenuItem _menuService

            )
        {
            orderServises = _orderServises;
            repo = _repo;
            tableService = _tableService;
            subOrderServises = _subOrderServises;
            categoryService = _categoryService;
            menuService = _menuService;
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

            await tableService.SaveCurrentOrderIdToTable(model.Id, Id);

            return RedirectToAction("Details", new { id = model.Id });
            
        }

        [HttpGet]
        public async Task<IActionResult>  Details(int Id)
        {
            var order = await orderServises.GetOrderByIdAsync(Id);

            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> Purchase(int Id)
        {
            var menuItems = await menuService.GetAllMenuItemsPurchaseAsync();

            var model = await categoryService.GetAllCategoriesSubOrderAsync();

            foreach (var item in model)
            {
                item.MenuItems = menuItems.Where(x => x.CategoryId == item.Id).ToList();
            }

            ViewBag.SubOrderId = Id;

            foreach (var item in model)
            {
                item.SubOrderId = Id;
               
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(IEnumerable< Category> model, int Id)
        {
            if (!ModelState.IsValid)
            {

                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(model);
            }


            try
            {
                var orderId = await subOrderServises.AddCategoriesToSubOrderAsync(model, Id);

                return RedirectToAction("Details", new { Id = orderId });

            }
            catch (Exception ex)
            {

                return RedirectToPage("Error", ex);
            }

            
        }

        public IActionResult Test(TempOrder model)
        {
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateSubOrder(int Id)
        {
            var model = new TempOrder();

            ViewBag.OrderId = Id;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubOrder(SubOrder model, int Id)
        {
            var subId = await subOrderServises.CreateSubOrderAsync(model, Id);

            return RedirectToAction("Purchase", new { id = subId });
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await orderServises.CloseTheOrder(Id);

            return RedirectToAction("AllTables", "Home");
        }
    }
}
