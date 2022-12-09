using RestaurantManagmentSystem.Core.Constrains.Table;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string? UserId { get; set; } = null!;

        [ForeignKey(nameof(Order))]
        public int? OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public ICollection<Customer> Customers { get; set; } = new List<Customer>();

    }
}
