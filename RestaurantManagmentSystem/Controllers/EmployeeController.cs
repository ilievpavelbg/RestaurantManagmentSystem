using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Models.ApplicationUser;
using RestaurantManagmentSystem.Core.Models.Employee;

namespace RestaurantManagmentSystem.Controllers
{

    [Authorize(Roles = "Administrator")]
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
        public async Task<IActionResult> All()
        {
            var employees = await employeeService.GetAllEmployeesAsync();

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new EmployeeViewModel
            {
                Departments = await departmentService.GetAllDepartmentsAsync()
            };

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

        public async Task<IActionResult> Details(int Id)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(Id);

            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            try
            {
                var employee = await employeeService.GetEmployeeByIdAsync(Id);

                return View(employee);

            }
            catch (Exception ex)
            {

                TempData["message"] = ex.Message;

                return RedirectToAction("All");
            }

        }
        /// <summary>
        /// Edit Department, update database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details");
            }

            await employeeService.EditPostEmployeeAsync(model);

            return RedirectToAction("Details");
        }
        /// <summary>
        /// Delete Department
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

    }
}
