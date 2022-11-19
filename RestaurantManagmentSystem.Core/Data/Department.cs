using RestaurantManagmentSystem.Core.Constrains.Department;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Core.Data
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(DepartmentConstrains.NameMaxLenght)]
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
