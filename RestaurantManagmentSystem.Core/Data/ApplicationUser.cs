using Microsoft.AspNetCore.Identity;
using RestaurantManagmentSystem.Core.Constrains.User;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Core.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(ApplicationUserConstrains.FirstNameMaxLenght)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(ApplicationUserConstrains.LastNameMaxLenght)]
        public string LastName { get; set; } = null!;

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; } = null!;
    }
}
