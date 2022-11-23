namespace RestaurantManagmentSystem.Core.Models.Employee
{
    public class AllEmployeeViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int? DepartmentId { get; set; }

        public string UserName { get; set; } = null!;

        public string? AppUserId { get; set; } = null!;

    }
}
