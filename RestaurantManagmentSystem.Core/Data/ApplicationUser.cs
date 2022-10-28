using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagmentSystem.Core.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [Column(TypeName = "date")]
        public DateTime HireDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LeaveDate { get; set; }

        [Required]
        [Range(typeof(decimal), "0.00", "10000.00")]
        public decimal Salary { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = null!;

        [StringLength(50)]
        public string? Address { get; set; }

        [StringLength(50)]
        public string? Town { get; set; }

        public bool IsDeleted { get; set; }
    }
}
