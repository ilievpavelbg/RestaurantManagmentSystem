using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Models.ApplicationUser;

namespace RestaurantManagmentSystem.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly IEmployee employeeService;
        private readonly IDepartment departmentService;

        public EmployeeController(IEmployee _userService, IDepartment _departmentService)
        {
            employeeService = _userService;
            departmentService = _departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new EmployeeViewModel();

            model.Departments = await departmentService.GetAllDepartmentsAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = await departmentService.GetAllDepartmentsAsync();

                return View(model);
            }

            await employeeService.CreateUserAsync(model);

            return RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult All()
        {

            return View();
        }
    }
}
