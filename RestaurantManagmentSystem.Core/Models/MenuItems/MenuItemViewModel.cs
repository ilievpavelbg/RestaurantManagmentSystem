namespace RestaurantManagmentSystem.Core.Models.MenuItems
{

    public class MenuItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string ImageURL { get; set; } = null!;

        public bool ItemsForCooking { get; set; }

        public string CategoryName { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
