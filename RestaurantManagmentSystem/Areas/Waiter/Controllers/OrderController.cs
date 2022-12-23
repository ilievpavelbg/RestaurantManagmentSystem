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
        private readonly ITempOrder tempOrderService;

        public OrderController(
            IOrder _orderServises, 
            IRepository _repo,
            ITable_1 _tableService,
            ISubOrder _subOrderServises,
            ICategory _categoryService,
            IMenuItem _menuService,
            ITempOrder _tempOrderService

            )
        {
            orderServises = _orderServises;
            repo = _repo;
            tableService = _tableService;
            subOrderServises = _subOrderServises;
            categoryService = _categoryService;
            menuService = _menuService;
            tempOrderService = _tempOrderService;
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

        public async Task<IActionResult> Details(int Id)
        {
            var order = await orderServises.GetOrderByIdAsync(Id);

            if(order == null)
            {
                return RedirectToAction("Create");
            }
            

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
                await subOrderServises.AddCategoriesToSubOrderAsync(model, Id);

                //await subOrderServises.AddSubOrderToOrderAsync(model.OrderId);

                //var subId = model.OrderId;

                //return RedirectToAction("Details", new { id = subId });

            }
            catch (Exception ex)
            {

                return RedirectToPage("Error", ex);
            }

            return RedirectToAction("Test");
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
    }
}
