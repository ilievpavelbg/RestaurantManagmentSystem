namespace RestaurantManagmentSystem.Core.Models.MenuItems
{
    public class SubOrderMenuItemsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int? OnStock { get; set; }
        public int? OrderedQty { get; set; }

        public decimal Price { get; set; }

        public string ImageURL { get; set; } = null!;

        public bool ItemsForCooking { get; set; }

        public bool IsChecked { get; set; }

        public bool IsDeleted { get; set; }

        public int CategoryId { get; set; }

    }
}
