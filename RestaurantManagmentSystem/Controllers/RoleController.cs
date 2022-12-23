using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Roles;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : Controller
    {
        private IRole roleService;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Initialize Role Manager and User Manager
        /// </summary>
        /// <param name="_roleManager"></param>
        /// <param name="_userManager"></param>
        /// <param name="_roleService"></param>
        public RoleController(RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            IRole _roleService)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            roleService = _roleService;
        }

        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await roleService.AllRolesAsync();

            return View(model);
        }

        /// <summary>
        /// Get all user and their roles
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        public async Task<IActionResult> UserRole()
        {
            var users = userManager.Users.ToList();

            var userRoleModel = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var newUserRoleModel = new UserRolesViewModel();

                newUserRoleModel.UserId = user.Id;
                newUserRoleModel.FirstName = user.FirstName;
                newUserRoleModel.LastName = user.LastName;
                newUserRoleModel.UserName = user.UserName;
                newUserRoleModel.Roles = await GetUserRoles(user);

                userRoleModel.Add(newUserRoleModel);

            }

            return View(userRoleModel);
        }

        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {

            var usersRoles = (List<string>)await userManager.GetRolesAsync(user);

            return usersRoles;
        }


        [HttpGet]
        public IActionResult Create() => View();

        /// <summary>
        /// Create Role
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Errors(result);
                }

            }
            return View(name);
        }

        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Errors(result);
                }

            }
            else
                ModelState.AddModelError("", "No role found");

            return View("Index", roleManager.Roles);
        }

        /// <summary>
        /// Add or remove user from role
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string userId)
        {
            var model = await roleService.EditUserRoleAsync(userId);

            return View(model);
        }

        /// <summary>
        /// Add or remove user from role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRolesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong. Try again !");

                return View("Edit");
            }

            try
            {
                await roleService.EditPostUserRoleAsync(model);

                return RedirectToAction("UserRole");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);

                return View(ex.Message);
            }
            
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
