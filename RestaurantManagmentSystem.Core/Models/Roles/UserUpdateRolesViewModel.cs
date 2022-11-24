namespace RestaurantManagmentSystem.Core.Models.Roles
{
    public class UserUpdateRolesViewModel
    {
        public string UserId { get; set; } = null!;
        public List<ManageUserRolesViewModel> Roles { get; set; }
    }
}
