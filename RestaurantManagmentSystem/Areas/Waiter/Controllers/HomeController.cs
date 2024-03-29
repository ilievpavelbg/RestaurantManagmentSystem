﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Common;
using RestaurantManagmentSystem.Core.Contracts;

namespace RestaurantManagmentSystem.Areas.Waiter.Controllers
{
    [Area("Waiter")]
    [Authorize(Roles = "Waiter, Manager")]
    public class HomeController : Controller
    {
        private readonly ITable_1 tableService;
        private readonly IOrder orderService;

        /// <summary>
        /// Initialize category and manu services in constructor
        /// </summary>
        /// <param name="_category"></param>
        /// <param name="_menuItem"></param>
        public HomeController(ITable_1 _tableService, IOrder _orderService)
        {
            tableService = _tableService;
            orderService = _orderService;
        }
        /// <summary>
        /// Get all tables
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AllTables()
        {

            var userId = User.Id();

            ViewBag.UserId = userId;

            var allTables = await tableService.GetAllTablesAsync();

            return View(allTables);
        }

        /// <summary>
        /// Reserve table
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Reserve(int Id)
        {
            try
            {
                var userId = User.Id();

                ViewBag.UserId = userId;

                var table = await tableService.GetTableByIdAsync(Id);

                await tableService.ReserveTableAsync(table.Id, userId);

                return RedirectToAction("AllTables");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorInfo = ex.Message;

                return RedirectToAction("AllTables");
            }
        }

        /// <summary>
        /// Release table
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Release(int Id)
        {
            try
            {
                var hasOrder = orderService.GetOrderIdByTableId(Id);

                ViewBag.OrderId = hasOrder;


                var userId = User.Id();

                ViewBag.UserId = userId;

                var table = await tableService.GetTableByIdAsync(Id);

                await tableService.ReleaseTableAsync(table.Id, userId);

                return RedirectToAction("AllTables");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorInfo = ex.Message;

                return RedirectToAction("AllTables");

            }


        }

    }
}
