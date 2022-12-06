using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Models.Tables;

namespace RestaurantManagmentSystem.Controllers
{
    public class TableController : Controller
    {
        private readonly ITable_1 tableService;
        /// <summary>
        /// Initialize table service
        /// </summary>
        /// <param name="_tableService"></param>
        public TableController(ITable_1 _tableServise)
        {
            tableService = _tableServise;
        }

        public async Task<IActionResult> Index()
        {
            var model = await tableService.GetAllTablesAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateTableViewModel()
            {
                IsDeleted = false,
                IsReserved = false
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTableViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await tableService.CreateTableAsync(model);

            return RedirectToAction("Index");

        }
    }
}
