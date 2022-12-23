using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Models.Departments;

namespace RestaurantManagmentSystem.Controllers
{
    [Authorize(Roles = "Administrator")]
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
        public async Task<IActionResult> Add()
        {
            var model = new MultipleDepartmentViewModel
            {
                DepartmentModel = new DepartmentViewModel(),
                ActiveDepartments = await departmentService.GetAllDepartmentsAsync(),
                DeletedDepartments = await departmentService.GetAllDeletedDepartmentsAsync()
            };

            return View(model);
        }
        /// <summary>
        /// Add Department to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(MultipleDepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            if (await departmentService.HasThisEntityAsync(model.DepartmentModel.Name))
            {
                TempData["Error"] = "Alredy has Department with this name. Try with other one !";

                return RedirectToAction("Add");
            }

            await departmentService.CreateDepartmentAsync(model.DepartmentModel);

            return RedirectToAction("Add");
        }
        /// <summary>
        /// Show all Departments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var allDepartments = await departmentService.GetAllDepartmentsAsync();

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
            try
            {
                var department = await departmentService.EditGetDepartmentAsync(Id);

                return View(department);

            }
            catch (Exception ex)
            {

                TempData["message"] = ex.Message;

                return RedirectToAction("Add");
            }
            
        }
        /// <summary>
        /// Edit Department, update database
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

            return RedirectToAction("Add");
        }
        /// <summary>
        /// Delete Department
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int Id)
        {
            var department = await departmentService.GetDepartmentById(Id);

            try
            {
                await departmentService.DeleteDepartmentAsync(Id);

                TempData["message"] = $"Succesfully deleted {department.Name}";

                return RedirectToAction("Add");
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return RedirectToAction("Add");
            }

        }
        /// <summary>
        /// Restore Department
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Restore(int Id)
        {
            await departmentService.RestoreDepartmentAsync(Id);

            return RedirectToAction("Add");
        }
    }
}
