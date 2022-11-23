using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Roles;

namespace RestaurantManagmentSystem.Core.Services
{
    public class RoleService : IRole
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public RoleService(RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            IEmployee _employeeService)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }
        public async Task<IEnumerable<AllRolesViewModel>> AllRolesAsync()
        {
            var roles = await roleManager.Roles.ToListAsync();

            var modelList = new List<AllRolesViewModel>();

            foreach (var role in roles)
            {
                List<string> names = new List<string>();

                if (role != null)
                {
                    foreach (var user in userManager.Users)
                    {
                        if (user != null && await userManager.IsInRoleAsync(user, role.Name))

                            names.Add(user.UserName);
                    }
                }

                var model = new AllRolesViewModel()
                {
                    RoleId = role.Id,
                    Name = role.Name,
                    UsersInRole = names
                };

                modelList.Add(model);
            }
           

            return modelList;
        }
    }
}
