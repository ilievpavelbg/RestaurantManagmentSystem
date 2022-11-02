using RestaurantManagmentSystem.Core.Constrains.Table;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Core.Data
{
    public class Table
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(TableConstrains.NumberMinLenght, TableConstrains.NumberMaxLenght)]
        public int Number { get; set; }

        [Required]
        [Range(TableConstrains.CapacityMinLenght, TableConstrains.CapacityMaxLenght)]
        public int Capacity { get; set; }

        [Required]
        public bool IsReserved { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Customer> Customers { get; set; } = new List<Customer>();

    }
}
