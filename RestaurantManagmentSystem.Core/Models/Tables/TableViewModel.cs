namespace RestaurantManagmentSystem.Core.Models.Tables
{
    public class TableViewModel
    {
        public int Id { get; set; }
        public bool IsReserved { get; set; }
        public string? UserId { get; set; }
        public int? OrderId { get; set; }
    }
}
