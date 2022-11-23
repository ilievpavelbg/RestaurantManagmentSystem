namespace RestaurantManagmentSystem.Core.Models.Roles
{
    public class AllRolesViewModel
    {
        public string RoleId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public IEnumerable<string> UsersInRole { get; set; } = new List<string>();  
    }
}
