using RestaurantManagmentSystem.Core.Constrains.Department;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Core.Models.Departments
{
    public class DepartmentViewModel
    {
        [Required]
        [StringLength(DepartmentConstrains.NameMaxLenght, MinimumLength = DepartmentConstrains.NameMinLenght)]
        public string Name { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
