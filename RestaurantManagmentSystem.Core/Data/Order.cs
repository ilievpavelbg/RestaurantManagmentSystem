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

        [Required]
        public DateTime ClosedOn { get; set; }

        [Required]
        public bool OrderStatus { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string? ApplicationUserId { get; set; } = null!;
        public ApplicationUser? ApplicationUser { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Table))]
        public int TableId { get; set; }
        public Table Table { get; set; } = null!;

        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
