using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.User;

namespace RestaurantManagmentSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly IEmployee employeeService;

        public UserController(
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager,
            IEmployee _employeeService)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            employeeService = _employeeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {

            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existUser = await userManager.FindByEmailAsync(model.Email);

            if (existUser != null)
            {
                if (existUser.Email == model.Email)
                {
                    ModelState.AddModelError("", "Already exist user with this email");

                    return View(model);
                }
            }
           


            var user = new ApplicationUser()
            {
                Email = model.Email,
                FirstName = model.FirstNane,
                LastName = model.LastNane,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "User");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                var hasEmployee = await employeeService.EsistEmployeeByEmailAsync(user.Email);
                
                if (result.Succeeded)
                {
                    if (hasEmployee)
                    {
                        var employee = await employeeService.GetEmployeeByEmailAsync(user.Email);

                        if (user.Email == employee.Email && employee != null && user.EmployeeId == null)
                        {
                            await employeeService.ConnectUserWithEmployeeAsync(employee, user);

                        }
                    }

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid login");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
