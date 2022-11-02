using RestaurantManagmentSystem.Core.Constrains.Customer;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Core.Data
{
    public class Customer
    {
        [Key]
        public int? Id { get; set; }

        [StringLength(CustomerConstrains.NameMaxLenght)]
        public string? Name { get; set; } = null!;

        [StringLength(CustomerConstrains.PhoneMaxLenght)]
        public string? Phone { get; set; } = null!;

        [StringLength(CustomerConstrains.EmailMaxLenght)]
        public string? Email { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public Table? Table { get; set; } = null!;
    }
}
