using RestaurantManagmentSystem.Core.Constrains.User;
using RestaurantManagmentSystem.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Core.Models.ApplicationUser
{
    public class ApplicationUserCreateModel
    {
        [Required]
        [StringLength(UserConstrains.FirstNameMaxLenght, MinimumLength = UserConstrains.FirstNameMinLenght)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(UserConstrains.LastNameMaxLenght, MinimumLength = UserConstrains.LastNameMinLenght)]
        public string LastName { get; set; } = null!;

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        [Range(typeof(decimal), UserConstrains.SalaryMinLenght, UserConstrains.SalaryMinLenght)]
        public decimal Salary { get; set; }

        [Required]
        [StringLength(UserConstrains.PhoneMaxLenght, MinimumLength = UserConstrains.PhoneMinLenght)]
        public string Phone { get; set; } = null!;

        [StringLength(UserConstrains.AddressMaxLenght, MinimumLength = UserConstrains.AddressMinLenght)]
        public string? Address { get; set; }

        [StringLength(UserConstrains.TownMaxLenght, MinimumLength = UserConstrains.TownMinLenght)]
        public string? Town { get; set; }

        public ICollection<Department> Departments { get; set; } = new List<Department>();

    }
}
