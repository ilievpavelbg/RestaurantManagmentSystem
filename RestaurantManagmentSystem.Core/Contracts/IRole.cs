using Microsoft.AspNetCore.Identity;
using RestaurantManagmentSystem.Core.Models.Roles;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface IRole
    {
        Task<IEnumerable<AllRolesViewModel>> AllRolesAsync();
    }
}
