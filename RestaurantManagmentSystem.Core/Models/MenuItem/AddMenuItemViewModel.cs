using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagmentSystem.Core.Models.MenuItem
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

        public string Category { get; set; } = null!;

    }
}
