using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Core.Data
{
    public class Table
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 10)]
        public int Number { get; set; }

        [Required]
        [Range(2, 10)]
        public int Capacity { get; set; }

        [Required]
        public bool IsReserved { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Customer> Customers { get; set; } = new List<Customer>();

    }
}
