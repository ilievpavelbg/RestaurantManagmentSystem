using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Models.Departments;

namespace RestaurantManagmentSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartment departmentService;
        /// <summary>
        /// Initialize department service
        /// </summary>
        /// <param name="_departmentService"></param>
        public DepartmentController(IDepartment _departmentService)
        {
            departmentService = _departmentService;
        }
        /// <summary>
        /// Department Index page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return RedirectToAction("All");
        }
        /// <summary>
        /// Add Department View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            var model = new DepartmentViewModel();

            return View(model);
        }
        /// <summary>
        /// Add Department to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(DepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TempData["Result"] = "this is a test";

            await departmentService.AddDepartmentAsync(model);

            return View(model);
        }
        /// <summary>
        /// Show all departments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult All()
        {
            var allDepartments = departmentService.GetAllDepartments();

            return View(allDepartments);
        }
        /// <summary>
        /// Edit Department View
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var dept = await departmentService.EditGetDepartmentAsync(Id);

            return View(dept);
        }
        /// <summary>
        /// Edit department, update database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditDepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await departmentService.EditPostDepartmentAsync(model);

            return RedirectToAction("All");
        }
        /// <summary>
        /// Delete department
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int Id)
        {
            return View ();

        }
    }
}
