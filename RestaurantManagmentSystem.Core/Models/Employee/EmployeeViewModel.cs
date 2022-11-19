using RestaurantManagmentSystem.Core.Constrains.Employee;
using RestaurantManagmentSystem.Core.Models.Departments;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Core.Models.ApplicationUser
{
    public class EmployeeViewModel
    {
        [Required]
        [StringLength(EmployeeConstrains.FirstNameMaxLenght, MinimumLength = EmployeeConstrains.FirstNameMinLenght)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(EmployeeConstrains.LastNameMaxLenght, MinimumLength = EmployeeConstrains.LastNameMinLenght)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public string HireDate { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), EmployeeConstrains.SalaryMinLenght, EmployeeConstrains.SalaryMaxLenght)]
        public decimal Salary { get; set; }

        [Required]
        [StringLength(EmployeeConstrains.PhoneMaxLenght, MinimumLength = EmployeeConstrains.PhoneMinLenght)]
        public string Phone { get; set; } = null!;

        [StringLength(EmployeeConstrains.AddressMaxLenght, MinimumLength = EmployeeConstrains.AddressMinLenght)]
        public string? Address { get; set; }

        [StringLength(EmployeeConstrains.TownMaxLenght, MinimumLength = EmployeeConstrains.TownMinLenght)]
        public string? Town { get; set; }

        public int? DepartmentId { get; set; }

        public IEnumerable<EditDepartmentViewModel> Departments { get; set; } = new List<EditDepartmentViewModel>();

    }
}
