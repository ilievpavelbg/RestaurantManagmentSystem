using RestaurantManagmentSystem.Core.Constrains.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagmentSystem.Core.Data
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(EmployeeConstrains.FirstNameMaxLenght)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(EmployeeConstrains.LastNameMaxLenght)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(EmployeeConstrains.PhoneMaxLenght, MinimumLength = EmployeeConstrains.PhoneMinLenght)]
        public string Phone { get; set; } = null!;


        [Required]
        [Column(TypeName = "date")]
        public DateTime HireDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LeaveDate { get; set; }

        [Required]
        [Range(typeof(decimal), EmployeeConstrains.SalaryMinLenght, EmployeeConstrains.SalaryMaxLenght)]
        public decimal Salary { get; set; }

        [StringLength(EmployeeConstrains.AddressMaxLenght)]
        public string? Address { get; set; }

        [StringLength(EmployeeConstrains.TownMaxLenght)]
        public string? Town { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; } = null!;

        public int? OrderId { get; set; }
        public ICollection<Order>? Orders { get; set; } = new List<Order>();

        [ForeignKey(nameof(ApplicationUser))]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; } = null!;
    }
}
