using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Roles;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Controllers
{
    public class RoleController : Controller
    {
        private IRole roleService;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            IRole _roleService)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            roleService = _roleService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await roleService.AllRolesAsync();

            return View(model);
        }

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

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
