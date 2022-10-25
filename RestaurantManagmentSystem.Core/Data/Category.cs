using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderManagmentSystem.Core.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
