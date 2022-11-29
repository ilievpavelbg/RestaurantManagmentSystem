namespace RestaurantManagmentSystem.Core.Models.Employee
{
    public class EmployeeDetailsViewModel
    {
        public int Id  { get; set; }
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string HireDate { get; set; } = null!;

        public decimal Salary { get; set; }

        public string Phone { get; set; } = null!;
        public string? Address { get; set; }
        public string? Town { get; set; }

        public string Department { get; set; } = null!;

    }
}
