using System.ComponentModel.DataAnnotations;


namespace RestaurantManagmentSystem.Core.Models.Categories
{
    public class CategoryViewModel
    {

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = null!;
    }
}
