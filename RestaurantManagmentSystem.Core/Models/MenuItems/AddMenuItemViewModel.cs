using Microsoft.AspNetCore.Http;
using RestaurantManagmentSystem.Core.Constrains.MenuItem;
using RestaurantManagmentSystem.Core.Models.Categories;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Core.Models.MenuItems
{

    public class AddMenuItemViewModel
    {
        [Required]
        [StringLength(MenuItemConstrains.NameMaxLenght, MinimumLength = MenuItemConstrains.NameMinLenght)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(MenuItemConstrains.DescriptionMaxLenght, MinimumLength = MenuItemConstrains.DescriptionMinLenght)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), MenuItemConstrains.PriceMinLenght, MenuItemConstrains.PriceMaxLenght)]
        public decimal Price { get; set; }

        public string ImageURL { get; set; } = null!;

        [Required]
        public bool ItemsForCooking { get; set; }

        public int? CategoryId { get; set; }

        public IEnumerable<EditCategoryViewModel> Categories { get; set; } = new List<EditCategoryViewModel>();

    }
}
