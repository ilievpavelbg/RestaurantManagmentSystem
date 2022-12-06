using RestaurantManagmentSystem.Core.Models.Roles;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface IRole
    {
        Task<IEnumerable<AllRolesViewModel>> AllRolesAsync();
        Task<UserUpdateRolesViewModel> EditUserRoleAsync(string userId);
        Task EditPostUserRoleAsync(UserUpdateRolesViewModel model);
    }
}
