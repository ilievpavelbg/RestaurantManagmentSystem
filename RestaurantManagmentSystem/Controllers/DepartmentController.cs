﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Add()
        {
            var model = new EditDepartmentViewModel();

            var allDepartments = await departmentService.GetAllDepartmentAsync();
            var allDeletedDepartments = await departmentService.GetAllDeletedDepartmentAsync();

            ViewBag.ActiveDepartments = allDepartments;
            ViewBag.DeletedDepartments = allDeletedDepartments;

            return View(model);
        }
        /// <summary>
        /// Add Department to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(EditDepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (departmentService.HasThisEntity(model))
            {
                ModelState.AddModelError("", "Alredy has entity with this name !");
                return View(model);
            }

            await departmentService.AddDepartmentAsync(model);

            //return View(model);
            return RedirectToAction("Add");
        }
        /// <summary>
        /// Show all departments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var allDepartments = await departmentService.GetAllDeletedDepartmentAsync();

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
            var department = await departmentService.EditGetDepartmentAsync(Id);

            return View(department);
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

            return RedirectToAction("All");
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

                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return RedirectToAction("All");
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
