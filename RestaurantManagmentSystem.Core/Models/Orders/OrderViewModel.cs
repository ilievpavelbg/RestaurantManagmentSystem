using RestaurantManagmentSystem.Core.Models.MenuItems;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Core.Models.Orders
{
    public class OrderViewModel
    {
        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int TableId { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public IEnumerable<OrderMenuItemViewModel> MenuItems { get; set; } = new List<OrderMenuItemViewModel>();
    }
}
