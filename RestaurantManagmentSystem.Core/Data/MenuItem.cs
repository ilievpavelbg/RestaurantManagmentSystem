using RestaurantManagmentSystem.Core.Constrains.MenuItem;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagmentSystem.Core.Data
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MenuItemConstrains.NameMaxLenght)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(MenuItemConstrains.DescriptionMaxLenght)]
        public string Description { get; set; } = null!;

        public int? OnStock { get; set; }
        public int? OrderedQty { get; set; }

        [Required]
        [Range(typeof(decimal), MenuItemConstrains.PriceMinLenght, MenuItemConstrains.PriceMaxLenght)]
        public decimal Price { get; set; }

        [Required]
        public string ImageURL { get; set; } = null!;

        [Required]
        public bool ItemsForCooking { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
