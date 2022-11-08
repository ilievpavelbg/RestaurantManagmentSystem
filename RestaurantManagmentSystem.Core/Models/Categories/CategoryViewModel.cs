using RestaurantManagmentSystem.Core.Constrains.Category;
using System.ComponentModel.DataAnnotations;


namespace RestaurantManagmentSystem.Core.Models.Categories
{
    public class CategoryViewModel
    {

        [Required]
        [StringLength(CategoryConstrains.NameMaxLenght, MinimumLength = CategoryConstrains.NameMinLenght)]
        public string Name { get; set; } = null!;
    }
}
