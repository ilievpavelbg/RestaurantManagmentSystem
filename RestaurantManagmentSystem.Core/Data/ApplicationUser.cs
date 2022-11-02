using Microsoft.AspNetCore.Identity;
using RestaurantManagmentSystem.Core.Constrains.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagmentSystem.Core.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(UserConstrains.FirstNameMaxLenght)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(UserConstrains.LastNameMaxLenght)]
        public string LastName { get; set; } = null!;

        [Required]
        [Column(TypeName = "date")]
        public DateTime HireDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LeaveDate { get; set; }

        [Required]
        [Range(typeof(decimal), UserConstrains.SalaryMinLenght, UserConstrains.SalaryMinLenght)]
        public decimal Salary { get; set; }

        [Required]
        [StringLength(UserConstrains.PhoneMaxLenght)]
        public string Phone { get; set; } = null!;

        [StringLength(UserConstrains.AddressMaxLenght)]
        public string? Address { get; set; }

        [StringLength(UserConstrains.TownMaxLenght)]
        public string? Town { get; set; }

        public bool IsDeleted { get; set; }
    }
}
