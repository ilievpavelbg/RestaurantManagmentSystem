using RestaurantManagmentSystem.Core.Constrains.Category;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Core.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryConstrains.NameMaxLenght)]
        public string Name { get; set; } = null!;

        public bool IsChecked { get; set; }
        public bool IsDeleted { get; set; }

        public int? SubOrderId { get; set; }
        public SubOrder SubOrder { get; set; } = null!;
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
