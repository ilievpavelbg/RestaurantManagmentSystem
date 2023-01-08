using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;

namespace RestaurantManagmentSystem.Areas.Chef.Controllers
{
    [Area("Chef")]

    [Authorize(Roles = "Chef, Manager")]

    public class HomeController : Controller
    {
        private readonly ISubOrder subOrder;

        public HomeController(ISubOrder _subOrder)
        {
            subOrder = _subOrder;
        }

        public async Task<IActionResult> Index()
        {
            var result = await subOrder.GetAllSubOrdersChef();

            return View(result);
        }

        public async Task<IActionResult> Complete(int Id)
        {
            await subOrder.CompleteSubOrder(Id);

            return RedirectToAction("Index");
        }
    }
}
