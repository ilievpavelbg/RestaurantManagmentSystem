using RestaurantManagmentSystem.Core.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RestaurantManagmentSystem.Core.Data
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? ClosedOn { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Table))]
        public int TableId { get; set; }
        public Table Table { get; set; } = null!;

        public IEnumerable<SubOrder> SubOrders { get; set; } = new List<SubOrder>();
        public IEnumerable<TempOrder> TempOrders { get; set; } = new List<TempOrder>();
    }
}
