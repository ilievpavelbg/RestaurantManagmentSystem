using RestaurantManagmentSystem.Core.Constrains.Product;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Core.Data
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ProductConstrains.NameMaxLenght)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(ProductConstrains.QuantityMinLenght, ProductConstrains.QuantityMaxLenght)]
        public int Quantity { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
