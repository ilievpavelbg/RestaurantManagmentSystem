using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Roles;

namespace RestaurantManagmentSystem.Core.Services
{
    public class RoleService : IRole
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleService(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }

        public async Task<IEnumerable<AllRolesViewModel>> AllRolesAsync()
        {
            List<IdentityRole> roles = await roleManager.Roles.ToListAsync();

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

        public async Task EditPostUserRoleAsync(UserUpdateRolesViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                throw new ArgumentException($"User with Id = {model.UserId} cannot be found");
            }

            var roles = await userManager.GetRolesAsync(user);

            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                throw new ArgumentException("Cannot remove user existing roles");
            }

            result = await userManager.AddToRolesAsync(user, model.Roles.Where(x => x.Selected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                throw new ArgumentException("Cannot add selected roles to user");
            }
        }

        public async Task<UserUpdateRolesViewModel> EditUserRoleAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException($"User with Id = {userId} cannot be found");

            }

            var modelUser = new UserUpdateRolesViewModel()
            {
                UserId = userId,

            };

            var roles = await roleManager.Roles.ToListAsync();

            var userRoles = new List<ManageUserRolesViewModel>();

            foreach (var role in roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }

                userRoles.Add(userRolesViewModel);

            }

            modelUser.Roles = userRoles;

            return modelUser;
        }
    }
}
