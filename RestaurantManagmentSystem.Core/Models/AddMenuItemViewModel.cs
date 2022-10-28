using System.ComponentModel.DataAnnotations;
using RestaurantManagmentSystem.Core.Data;


namespace RestaurantManagmentSystem.Core.Models
{

    public class AddMenuItemViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(2000, MinimumLength = 10)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), "0.00", "100.00")]
        public decimal Price { get; set; }

        [Required]
        public string ImageURL { get; set; } = null!;

        [Required]
        public bool ItemsForCooking { get; set; }

        public int CategoryId { get; set; } 

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();

    }
}
