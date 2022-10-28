using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagmentSystem.Core.Data
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), "0.00", "100.00")]
        public decimal Price { get; set; }

        [Required]
        public string ImageURL { get; set; } = null!;

        [Required]
        public bool ItemsForCooking { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
