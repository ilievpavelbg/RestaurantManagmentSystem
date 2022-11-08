﻿using System.ComponentModel.DataAnnotations;
using RestaurantManagmentSystem.Core.Constrains.MenuItem;
using RestaurantManagmentSystem.Core.Data;


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

        [Required]
        public string ImageURL { get; set; } = null!;

        [Required]
        public bool ItemsForCooking { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();

    }
}