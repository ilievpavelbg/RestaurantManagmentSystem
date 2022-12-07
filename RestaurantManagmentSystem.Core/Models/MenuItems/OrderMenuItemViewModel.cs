namespace RestaurantManagmentSystem.Core.Models.MenuItems
{
    public class OrderMenuItemViewModel
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int? OrderedQty { get; set; }

        public decimal Price { get; set; }

        public bool ItemsForCooking { get; set; }

        public int CategoryId { get; set; }

    }
}
